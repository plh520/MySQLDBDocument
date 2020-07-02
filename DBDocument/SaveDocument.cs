using MySql.Data.MySqlClient;
using NPOI.OpenXmlFormats.Shared;
using NPOI.OpenXmlFormats.Wordprocessing;
using NPOI.SS.Formula.Functions;
using NPOI.XWPF.UserModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DBDocument
{
    public partial class SaveDocument : Form
    {
        #region 构造函数
        public SaveDocument()
        {
            InitializeComponent();
            lv_tables.SelectedIndexChanged += Lv_tables_SelectedIndexChanged;
        }

        #endregion

        #region 变量

        List<TableModel> tablesList = new List<TableModel>();

        #endregion

        #region 窗体加载
        private void Form1_Load(object sender, EventArgs e)
        {
            SetLVTable();
            SetLVTableInfo();
        }

        void SetLVTable()
        {
            lv_tables.View = View.Details;
            lv_tables.FullRowSelect = true;
            lv_tables.Columns.Add("表名称").Width = 200;
            lv_tables.Columns.Add("表描述").Width = 200;
        }

        void SetLVTableInfo()
        {
            lv_TableInfo.View = View.Details;
            lv_TableInfo.FullRowSelect = true;
            lv_TableInfo.Columns.Add("字段名称").Width = 100;
            lv_TableInfo.Columns.Add("类型").Width = 100;
            lv_TableInfo.Columns.Add("是否为null").Width = 80;
            lv_TableInfo.Columns.Add("是否主键").Width = 80;
            lv_TableInfo.Columns.Add("字段说明").Width = 150;
        }
        #endregion

        #region 登录、并且获取所有数据库
        private void btn_login_Click(object sender, EventArgs e)
        {
            #region 验证数据
            if (string.IsNullOrEmpty(txt_dataSource.Text))
            {
                MessageBox.Show("主机不能为空，请填写");
                txt_dataSource.Focus();
                return;
            }

            if (string.IsNullOrEmpty(txt_PORT.Text))
            {
                MessageBox.Show("端口不能为空，请填写");
                txt_PORT.Focus();
                return;
            }

            if (string.IsNullOrEmpty(txt_uid.Text))
            {
                MessageBox.Show("用户名不能为空，请填写");
                txt_uid.Focus();
                return;
            }
            if (string.IsNullOrEmpty(txt_pwd.Text))
            {
                MessageBox.Show("密码不能为空，请填写");
                txt_pwd.Focus();
                return;
            }
            #endregion

            try
            {
                string conn = $"data source={txt_dataSource.Text};PORT={txt_PORT.Text};uid={txt_uid.Text};pwd={txt_pwd.Text};";

                var dataBases = GetDataBases(conn);

                foreach (var item in dataBases)
                {
                    cmb_DBList.Items.Add(item.databasesName);
                }

                txt_dataSource.Enabled = txt_PORT.Enabled = txt_pwd.Enabled = txt_uid.Enabled = (dataBases != null && dataBases.Count > 0) ? false : true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        #endregion

        #region 重置
        private void brn_re_Click(object sender, EventArgs e)
        {
            txt_dataSource.Text = txt_PORT.Text = txt_pwd.Text = txt_uid.Text = "";
            txt_dataSource.Enabled = txt_PORT.Enabled = txt_pwd.Enabled = txt_uid.Enabled = true;
            cmb_DBList.Items.Clear();
            lv_tables.Items.Clear();
            lv_TableInfo.Items.Clear();
        }
        #endregion

        #region 改变数据库获取对应的表结构
        private void cmb_DBList_SelectedIndexChanged(object sender, EventArgs e)
        {
            tablesList.Clear();

            string dbName = cmb_DBList.Text;

            if (string.IsNullOrEmpty(dbName))
            {
                MessageBox.Show("请选择数据库");
                return;
            }

            string conn = $"data source={txt_dataSource.Text};database={dbName};PORT={txt_PORT.Text};uid={txt_uid.Text};pwd={txt_pwd.Text};";

            var tables = GetDBTables(conn, dbName);

            foreach (var item in tables)
            {
                ListViewItem lvi = new ListViewItem();

                lvi.Text = item.table_name;             //第一列

                lvi.SubItems.Add(item.table_comment);   //第二列

                this.lv_tables.Items.Add(lvi);

                tablesList.Add(item);
            }
        }

        #endregion

        #region 单击左边表名获取对应的字段

        /// <summary>
        /// 单击表名获取对应的字段
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Lv_tables_SelectedIndexChanged(object sender, EventArgs e)
        {
            lv_TableInfo.Items.Clear();

            var result = lv_tables.FocusedItem.SubItems[0].Text;

            string conn = $"data source={txt_dataSource.Text};PORT={txt_PORT.Text};uid={txt_uid.Text};pwd={txt_pwd.Text};";

            string strSql = $"SHOW FULL FIELDS FROM {cmb_DBList.Text}.{result}";

            var reader = MySqlHelper.ExecuteReader(conn, CommandType.Text, strSql);

            StringBuilder strDBName = new StringBuilder();
            while (reader.Read())
            {
                if (reader.HasRows)
                {
                    ListViewItem lvi = new ListViewItem();

                    lvi.Text = reader.GetString(0);//第一列
                    lvi.SubItems.Add(reader.GetString(1));//第二列
                    lvi.SubItems.Add(reader.GetString(3));//第三列
                    lvi.SubItems.Add(reader.GetString(4));//第四列
                    lvi.SubItems.Add(reader.GetString(8));//第五列
                    this.lv_TableInfo.Items.Add(lvi);
                }
            }
        }

        #endregion

        #region 保存成word
        private void btn_SaveWord_Click(object sender, EventArgs e)
        {
            var model = new ThreadModel()
            {
                dbName = cmb_DBList.Text,
                savePath = txt_SaveWordPath.Text,
                dataSource = txt_dataSource.Text,
                port = txt_PORT.Text,
                uid = txt_uid.Text,
                pwd = txt_pwd.Text,
            };

            //创建Thread对象
            Thread thread = new Thread(SaveWord);
            thread.IsBackground = true;
            //启动线程
            thread.Start(model);
        }

        #region 保存word文档

        void saveword1(string st)
        {
            string dbName = cmb_DBList.Text;
            SaveWord(dbName);
        }
        void SaveWord(object obj)
        {
            ThreadModel threadModel = obj as ThreadModel;

            if (string.IsNullOrEmpty(threadModel.dbName))
            {
                MessageBox.Show("请选择数据库");
                return;
            }

            if (string.IsNullOrEmpty(threadModel.savePath))
            {
                MessageBox.Show("word保存路径不能为空");
                return;
            }

            //创建document对象
            var doc = new XWPFDocument();

            //创建段落对象1
            var p1 = doc.CreateParagraph();
            p1.Alignment = ParagraphAlignment.CENTER; //字体居中

            var runTitle = p1.CreateRun();
            runTitle.IsBold = true;
            runTitle.SetText($"{threadModel.dbName} 数据库文档");
            runTitle.FontSize = 16;
            runTitle.SetFontFamily("宋体", FontCharRange.None); //设置雅黑字体

            doc.CreateParagraph();
            doc.CreateParagraph();

            string conn = $"data source={threadModel.dataSource};database={threadModel.dbName};PORT={threadModel.port};uid={threadModel.uid};pwd={threadModel.pwd};";

            try
            {
                var tables = (tablesList != null && tablesList.Count > 0) ? tablesList : GetDBTables(conn, threadModel.dbName);

                var tableInfolist = GetTableInfoList(conn, threadModel.dbName);

                int tableNum = 1;

                foreach (var table in tables)
                {
                    var infoList = tableInfolist.Where(x => x.TABLE_NAME.Equals(table.table_name)).ToList();

                    #region 创建表格，循环写入列名

                    #region 创建表格、设置宽度、合并

                    var tableContent = doc.CreateTable(infoList.Count + 2, 6);
                    //tableContent.Width = 1000 * 5;

                    tableContent.SetColumnWidth(0, 600);
                    tableContent.SetColumnWidth(1, 1500);
                    tableContent.SetColumnWidth(2, 1000);
                    tableContent.SetColumnWidth(3, 1500);
                    tableContent.SetColumnWidth(4, 1000);
                    tableContent.SetColumnWidth(5, 2500);

                    tableContent.GetRow(0).MergeCells(0, 5); //合并

                    #endregion

                    #region 设置背景颜色

                    tableContent.GetRow(0).GetCell(0).SetColor("#fbd4b4");
                    tableContent.GetRow(1).GetCell(0).SetColor("#fbd4b4");
                    tableContent.GetRow(1).GetCell(1).SetColor("#fbd4b4");
                    tableContent.GetRow(1).GetCell(2).SetColor("#fbd4b4");
                    tableContent.GetRow(1).GetCell(3).SetColor("#fbd4b4");
                    tableContent.GetRow(1).GetCell(4).SetColor("#fbd4b4");
                    tableContent.GetRow(1).GetCell(5).SetColor("#fbd4b4");

                    #endregion

                    #region 创建表头

                    tableContent.GetRow(0).GetCell(0).SetParagraph(SetCellText(doc, tableContent, $"表名({table.table_name})"));
                    tableContent.GetRow(1).GetCell(0).SetParagraph(SetCellText(doc, tableContent, "序号"));
                    tableContent.GetRow(1).GetCell(1).SetParagraph(SetCellText(doc, tableContent, "字段名称"));
                    tableContent.GetRow(1).GetCell(2).SetParagraph(SetCellText(doc, tableContent, "类型"));
                    tableContent.GetRow(1).GetCell(3).SetParagraph(SetCellText(doc, tableContent, "是否为null"));
                    tableContent.GetRow(1).GetCell(4).SetParagraph(SetCellText(doc, tableContent, "是否主键"));
                    tableContent.GetRow(1).GetCell(5).SetParagraph(SetCellText(doc, tableContent, "字段描述"));

                    #endregion

                    int i = 2;//从表格第三行开始写入列

                    #region 循环写入列

                    foreach (var item in infoList)
                    {
                        tableContent.GetRow(i).GetCell(0).SetParagraph(SetCellText(doc, tableContent, (i - 1).ToString(), ParagraphAlignment.CENTER, 50));//序号

                        tableContent.GetRow(i).GetCell(1).SetParagraph(SetCellText(doc, tableContent, item.column_name, ParagraphAlignment.CENTER, 50));//字段名称

                        tableContent.GetRow(i).GetCell(2).SetParagraph(SetCellText(doc, tableContent, item.column_type, ParagraphAlignment.CENTER, 50));//字段类型

                        tableContent.GetRow(i).GetCell(3).SetParagraph(SetCellText(doc, tableContent, item.IS_NULLABLE, ParagraphAlignment.CENTER, 50));//是否为null

                        tableContent.GetRow(i).GetCell(4).SetParagraph(SetCellText(doc, tableContent, item.COLUMN_KEY, ParagraphAlignment.CENTER, 50));//是否为主键

                        tableContent.GetRow(i).GetCell(5).SetParagraph(SetCellText(doc, tableContent, item.COLUMN_COMMENT, ParagraphAlignment.CENTER, 50));//字段描述

                        i++;

                        if (!lbl_info.InvokeRequired)
                        {
                            lbl_info.Text = $"正在导出Word文档请稍等，一共有({tables.Count})张表，已处理({tableNum})张表，已处理完({table.table_name})表";
                        }
                        else
                        {
                            lbl_info.Invoke(new Action(() =>
                            {
                                lbl_info.Text = $"正在导出Word文档请稍等，一共有({tables.Count})张表，已处理({tableNum})张表，已处理完({table.table_name})表";
                            }));
                        }
                        Thread.Sleep(5);
                    }
                    #endregion

                    tableNum++;

                    #endregion

                    doc.CreateParagraph();//新增段
                }

                #region 保存word文件
                string saveName = $"{threadModel.dbName}_数据库文档_{DateTime.Now.ToString("yyyy-MM-dd")}.doc";

                string path = Path.Combine(txt_SaveWordPath.Text, saveName);

                using (FileStream sw = File.Create(path))
                {
                    doc.Write(sw);
                    //sw.Close();
                }

                if (MessageBox.Show("保存成功，是否打开文件", "提示消息", MessageBoxButtons.OKCancel) == DialogResult.OK)
                {
                    Process.Start(path);
                }

                #endregion
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        #endregion


        #region 设置字体格式
        /// <summary>
        /// 设置字体格式
        /// </summary>
        /// <param name="doc"></param>
        /// <param name="table"></param>
        /// <param name="setText"></param>
        /// <returns></returns>
        public XWPFParagraph SetCellText(XWPFDocument doc, XWPFTable table, string setText)
        {
            //table中的文字格式设置
            var para = new CT_P();
            var pCell = new XWPFParagraph(para, table.Body);
            pCell.Alignment = ParagraphAlignment.CENTER; //字体居中
            pCell.VerticalAlignment = TextAlignment.CENTER; //字体居中

            var r1c1 = pCell.CreateRun();
            r1c1.IsBold = true;
            r1c1.SetText(setText);
            r1c1.FontSize = 12;
            r1c1.SetFontFamily("华文楷体", FontCharRange.None); //设置雅黑字体

            return pCell;
        }

        #endregion

        #region 设置单元格格式
        /// <summary>
        /// 设置单元格格式
        /// </summary>
        /// <param name="doc">doc对象</param>
        /// <param name="table">表格对象</param>
        /// <param name="setText">要填充的文字</param>
        /// <param name="align">文字对齐方式</param>
        /// <param name="textPos">rows行的高度</param>
        /// <returns></returns>
        public XWPFParagraph SetCellText(XWPFDocument doc, XWPFTable table, string setText, ParagraphAlignment align,
            int textPos)
        {
            var para = new CT_P();
            var pCell = new XWPFParagraph(para, table.Body);
            pCell.Alignment = align;

            var r1c1 = pCell.CreateRun();
            r1c1.SetText(setText);
            r1c1.FontSize = 8;
            r1c1.SetFontFamily("华文楷体", FontCharRange.None); //设置雅黑字体
            //r1c1.SetTextPosition(textPos); //设置高度

            return pCell;
        }
        #endregion

        #endregion

        #region 导出HTML文件
        private void btn_SaveHTML_Click(object sender, EventArgs e)
        {
            var model = new ThreadModel()
            {
                dbName = cmb_DBList.Text,
                savePath = txt_SaveHtmlPath.Text,
                dataSource = txt_dataSource.Text,
                port = txt_PORT.Text,
                uid = txt_uid.Text,
                pwd = txt_pwd.Text,
            };

            //创建Thread对象
            Thread thread = new Thread(SaveHtml);
            thread.IsBackground = true;
            //启动线程
            thread.Start(model);
        }

        void SaveHtml(object obj)
        {
            ThreadModel threadModel = obj as ThreadModel;

            string dbName = threadModel.dbName;

            #region 验证是否为空

            if (string.IsNullOrEmpty(dbName))
            {
                MessageBox.Show("请选择数据库");
                return;
            }

            if (string.IsNullOrEmpty(threadModel.savePath))
            {
                MessageBox.Show("HTML导出路径不能为空");
                return;
            }

            #endregion

            #region 获取数据

            StringBuilder str = new StringBuilder();
            str.Append("<div align=\"center\">");
            str.Append("<div>");
            str.Append($"<h1>{dbName} 数据库文档</h1>");
            str.Append("</div>");

            string conn = $"data source={threadModel.dataSource};database={dbName};PORT={threadModel.port};uid={threadModel.uid};pwd={threadModel.pwd};";

            var tables = (tablesList != null && tablesList.Count > 0) ? tablesList : GetDBTables(conn, dbName);

            var tableInfos = GetTableInfoList(conn, dbName);

            int tableNum = 1;
            foreach (var table in tables)
            {
                str.Append("<div>");
                str.Append("<table width=\"80%\" border=\"1\" cellpadding=\"0\" cellspacing=\"0\">");
                str.Append("<tr>");
                str.Append($"<td colspan=\"6\" align=\"center\" style=\"color: red; font - size:30px;\"><b> 表名({ table.table_name})</b></td>");
                str.Append("</tr>");
                str.Append("<tr align=\"center\" style=\"color:red;\">");
                str.Append("<td><b>序号</b></td>");
                str.Append("<td><b>字段名称</b></td>");
                str.Append("<td><b>类型</b></td>");
                str.Append("<td><b>是否为null</b></td>");
                str.Append("<td><b>是否主键</b></td>");
                str.Append("<td><b>字段描述</b></td>");
                str.Append("</tr>");

                #region 循环写入列名

                var infoList = tableInfos.Where(x => x.TABLE_NAME.Equals(table.table_name)).ToList();

                int i = 1;

                foreach (var info in infoList)
                {
                    str.Append("<tr align=\"center\">");
                    str.Append($"<td>{i}</td>");                    //序号
                    str.Append($"<td>{info.column_name}</td>");     //字段名称
                    str.Append($"<td>{info.column_type}</td>");     //字段类型
                    str.Append($"<td>{info.IS_NULLABLE}</td>");     //是否为null
                    str.Append($"<td>{info.COLUMN_KEY}</td>");      //是否为主键
                    str.Append($"<td>{info.COLUMN_COMMENT}</td>");  //字段描述
                    str.Append("</tr>");

                    i++;

                    if (!lbl_info.InvokeRequired)
                    {
                        lbl_info.Text = $"正在导出HTML文档请稍等，一共有({tables.Count})张表，已处理({tableNum})张表，已处理完({table.table_name})表";
                    }
                    else
                    {
                        lbl_info.Invoke(new Action(() =>
                        {
                            lbl_info.Text = $"正在导出HTML文档请稍等，一共有({tables.Count})张表，已处理({tableNum})张表，已处理完({table.table_name})表";
                        }));
                    }
                    Thread.Sleep(5);
                }

                str.Append("</table>");

                #endregion

                str.Append("<br/><br/>");

                tableNum++;
            }

            #endregion

            str.Append("</div>");

            #region 保存 HTML 文件

            string saveName = $"{dbName}_数据库文档_{DateTime.Now.ToString("yyyy-MM-dd")}.html";

            string path = Path.Combine(threadModel.savePath, saveName);

            using (FileStream fileStream = File.Create(path))
            {
                byte[] mybyte = Encoding.UTF8.GetBytes(str.ToString());
                fileStream.Write(mybyte, 0, mybyte.Length);
            }

            #endregion

            if (MessageBox.Show("保存成功，是否打开文件", "提示消息", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                var result1 = Process.Start("explorer.exe", path);
            }
        }
        #endregion

        #region 选择文件保存框
        private void textBox1_Click(object sender, EventArgs e)
        {
            string path = string.Empty;
            System.Windows.Forms.FolderBrowserDialog fbd = new System.Windows.Forms.FolderBrowserDialog();
            if (fbd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                path = fbd.SelectedPath;
            }

            txt_SaveWordPath.Text = path;
        }

        private void txt_SaveHtmlPath_Click(object sender, EventArgs e)
        {
            string path = string.Empty;
            System.Windows.Forms.FolderBrowserDialog fbd = new System.Windows.Forms.FolderBrowserDialog();
            if (fbd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                path = fbd.SelectedPath;
            }

            txt_SaveHtmlPath.Text = path;
        }
        #endregion

        #region 获取一个数据库所有表名

        public List<TableModel> GetDBTables(string conn, string dbName)
        {
            string strSql = $"SELECT table_name,table_comment FROM information_schema.tables WHERE table_schema = '{dbName}' ";

            var reader = MySqlHelper.ExecuteReader(conn, CommandType.Text, strSql);

            List<TableModel> list = new List<TableModel>();
            while (reader.Read())
            {
                if (reader.HasRows)
                {
                    list.Add(new TableModel()
                    {
                        table_name = reader.GetString(0),
                        table_comment = reader.GetString(1)
                    });
                }
            }
            return list;
        }
        #endregion

        #region 获取一个数据库所有字段名称

        public List<TableInfoModel> GetTableInfoList(string conn, string dbName)
        {
            List<TableInfoModel> list = new List<TableInfoModel>();

            string strSqlAll = $@"SELECT column_name,column_type,IS_NULLABLE,COLUMN_KEY,COLUMN_COMMENT,TABLE_NAME
            FROM information_schema.columns WHERE table_schema = '{dbName}'
            ORDER BY ORDINAL_POSITION asc";

            var result = MySqlHelper.ExecuteReader(conn, CommandType.Text, strSqlAll);

            while (result.Read())
            {
                if (result.HasRows)
                {
                    list.Add(new TableInfoModel()
                    {
                        column_name = result.GetString(0),
                        column_type = result.GetString(1),
                        IS_NULLABLE = result.GetString(2),
                        COLUMN_KEY = result.GetString(3),
                        COLUMN_COMMENT = result.GetString(4),
                        TABLE_NAME = result.GetString(5),
                    });
                }
            }

            return list;
        }
        #endregion

        #region 获取一个服务器所有数据库名称
        public List<DataBasesModel> GetDataBases(string conn)
        {
            string strSql = "show databases";

            var reader = MySqlHelper.ExecuteReader(conn, CommandType.Text, strSql);

            List<DataBasesModel> list = new List<DataBasesModel>();

            StringBuilder strDBName = new StringBuilder();
            while (reader.Read())
            {
                if (reader.HasRows)
                {
                    list.Add(new DataBasesModel() { databasesName = reader.GetString(0) });
                }
            }
            return list;
        }

        #endregion
    }

    #region 表字段详情

    public class TableInfoModel
    {
        /// <summary>
        /// 字段名称
        /// </summary>
        public string column_name { get; set; }
        /// <summary>
        /// 字段类型
        /// </summary>
        public string column_type { get; set; }
        /// <summary>
        /// 是否为null
        /// </summary>
        public string IS_NULLABLE { get; set; }
        /// <summary>
        /// 是否为主键
        /// </summary>
        public string COLUMN_KEY { get; set; }
        /// <summary>
        /// 字段描述
        /// </summary>
        public string COLUMN_COMMENT { get; set; }
        /// <summary>
        /// 表名
        /// </summary>
        public string TABLE_NAME { get; set; }
    }

    #endregion

    #region 表名称
    public class TableModel
    {
        /// <summary>
        /// 表名称
        /// </summary>
        public string table_name { get; set; }

        /// <summary>
        /// 表描述
        /// </summary>
        public string table_comment { get; set; }
    }
    #endregion

    #region 数据库名称
    public class DataBasesModel
    {
        /// <summary>
        /// 数据库名称
        /// </summary>
        public string databasesName { get; set; }
    }
    #endregion

    #region 线程所需数据
    public class ThreadModel
    {
        public string dbName { get; set; }
        public string savePath { get; set; }
        public string dataSource { get; set; }
        public string port { get; set; }
        public string uid { get; set; }
        public string pwd { get; set; }
    }
    #endregion
}
