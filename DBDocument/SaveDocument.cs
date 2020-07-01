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

        private void Lv_tables_SelectedIndexChanged(object sender, EventArgs e)
        {
            lv_TableInfo.Items.Clear();

            //lv_tables.SelectedIndexChanged -= Lv_tables_SelectedIndexChanged;

            var result = lv_tables.FocusedItem.SubItems[0].Text;

            //lv_tables.SelectedIndexChanged += Lv_tables_SelectedIndexChanged;

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

        #region 登录
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
                GetDBList();

                txt_dataSource.Enabled = txt_PORT.Enabled = txt_pwd.Enabled = txt_uid.Enabled = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        void GetDBList()
        {
            string strSql = "show databases";

            //string conn = $"data source=y3124150o2.qicp.vip;PORT=37689;uid=tujia;pwd=Tujiaerzaodb@;";
            string conn = $"data source={txt_dataSource.Text};PORT={txt_PORT.Text};uid={txt_uid.Text};pwd={txt_pwd.Text};";

            var reader = MySqlHelper.ExecuteReader(conn, CommandType.Text, strSql);

            StringBuilder strDBName = new StringBuilder();
            while (reader.Read())
            {
                if (reader.HasRows)
                {
                    cmb_DBList.Items.Add(reader.GetString(0));
                }
            }
        }
        #endregion

        #region 改变数据库获取对应的表结构
        private void cmb_DBList_SelectedIndexChanged(object sender, EventArgs e)
        {
            string dbName = cmb_DBList.Text;

            if (string.IsNullOrEmpty(dbName))
            {
                MessageBox.Show("请选择数据库");
                return;
            }

            GetDB_TableList(dbName);
        }

        void GetDB_TableList(string dbName)
        {
            string conn = $"data source={txt_dataSource.Text};database={cmb_DBList.Text};PORT={txt_PORT.Text};uid={txt_uid.Text};pwd={txt_pwd.Text};";

            string strSql = $"SELECT table_name,table_comment FROM information_schema.tables WHERE table_schema = '{dbName}' ";

            var reader = MySqlHelper.ExecuteReader(conn, CommandType.Text, strSql);

            while (reader.Read())
            {
                if (reader.HasRows)
                {
                    ListViewItem lvi = new ListViewItem();

                    lvi.Text = reader.GetString(0);

                    lvi.SubItems.Add(reader.GetString(1));

                    this.lv_tables.Items.Add(lvi);
                }
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

        #region 保存成word
        private void btn_SaveWord_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(cmb_DBList.Text))
            {
                MessageBox.Show("请选择数据库");
                return;
            }

            if (string.IsNullOrEmpty(txt_SaveWordPath.Text))
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
            runTitle.SetText($"{cmb_DBList.Text} 数据库文档");
            runTitle.FontSize = 16;
            runTitle.SetFontFamily("宋体", FontCharRange.None); //设置雅黑字体

            doc.CreateParagraph();
            doc.CreateParagraph();

            string conn = $"data source={txt_dataSource.Text};database={cmb_DBList.Text};PORT={txt_PORT.Text};uid={txt_uid.Text};pwd={txt_pwd.Text};";

            string strSql = $"SELECT table_name,table_comment FROM information_schema.tables WHERE table_schema = '{cmb_DBList.Text}' ";

            var reader = MySqlHelper.ExecuteReader(conn, CommandType.Text, strSql);
            int n = 1;
            while (reader.Read())
            {
                if (reader.HasRows)
                {
                    string tableName = reader.GetString(0);

                    #region 检验要素列表部分(数据库读取循环显示)

                    string strSqlCount = $@"select COUNT(*) AS NUM
from information_schema.columns where table_schema = '{cmb_DBList.Text}' and table_name = '{tableName}'";

                    var count = MySqlHelper.ExecuteScalar(conn, CommandType.Text, strSqlCount);

                    string strSql1 = $@"select column_name,column_type,IS_NULLABLE,COLUMN_KEY,COLUMN_COMMENT
from information_schema.columns where table_schema = '{cmb_DBList.Text}' and table_name = '{tableName}'
order by ORDINAL_POSITION asc";

                    var readerTabInfo = MySqlHelper.ExecuteReader(conn, CommandType.Text, strSql1);

                    var tableContent = doc.CreateTable(int.Parse(count.ToString()) + 2, 6);
                    tableContent.Width = 1000 * 5;
                    tableContent.SetColumnWidth(0, 600); /* 设置列宽 */
                    tableContent.SetColumnWidth(1, 1500); /* 设置列宽 */
                    tableContent.SetColumnWidth(2, 1000); /* 设置列宽 */
                    tableContent.SetColumnWidth(3, 1500); /* 设置列宽 */
                    tableContent.SetColumnWidth(4, 1000); /* 设置列宽 */
                    tableContent.SetColumnWidth(5, 2500); /* 设置列宽 */
                    tableContent.GetRow(0).MergeCells(0, 5); /* 合并行 */

                    tableContent.GetRow(0).GetCell(0).SetColor("#fbd4b4");
                    tableContent.GetRow(1).GetCell(0).SetColor("#fbd4b4");
                    tableContent.GetRow(1).GetCell(1).SetColor("#fbd4b4");
                    tableContent.GetRow(1).GetCell(2).SetColor("#fbd4b4");
                    tableContent.GetRow(1).GetCell(3).SetColor("#fbd4b4");
                    tableContent.GetRow(1).GetCell(4).SetColor("#fbd4b4");
                    tableContent.GetRow(1).GetCell(5).SetColor("#fbd4b4");

                    tableContent.GetRow(0).GetCell(0).SetParagraph(SetCellText(doc, tableContent, $"表名({tableName})"));
                    tableContent.GetRow(1).GetCell(0).SetParagraph(SetCellText(doc, tableContent, "序号"));
                    tableContent.GetRow(1).GetCell(1).SetParagraph(SetCellText(doc, tableContent, "字段名称"));
                    tableContent.GetRow(1).GetCell(2).SetParagraph(SetCellText(doc, tableContent, "类型"));
                    tableContent.GetRow(1).GetCell(3).SetParagraph(SetCellText(doc, tableContent, "是否为null"));
                    tableContent.GetRow(1).GetCell(4).SetParagraph(SetCellText(doc, tableContent, "是否主键"));
                    tableContent.GetRow(1).GetCell(5).SetParagraph(SetCellText(doc, tableContent, "字段描述"));

                    #region 循环写入列名

                    int i = 2;
                    while (readerTabInfo.Read())
                    {
                        if (readerTabInfo.HasRows)
                        {
                            tableContent.GetRow(i).GetCell(0).SetParagraph(SetCellText(doc, tableContent, (i - 1).ToString(), ParagraphAlignment.CENTER, 50));

                            tableContent.GetRow(i).GetCell(1).SetParagraph(SetCellText(doc, tableContent, readerTabInfo.GetString(0), ParagraphAlignment.CENTER, 50));

                            tableContent.GetRow(i).GetCell(2).SetParagraph(SetCellText(doc, tableContent, readerTabInfo.GetString(1), ParagraphAlignment.CENTER, 50));

                            tableContent.GetRow(i).GetCell(3).SetParagraph(SetCellText(doc, tableContent, readerTabInfo.GetString(2), ParagraphAlignment.CENTER, 50));

                            tableContent.GetRow(i).GetCell(4).SetParagraph(SetCellText(doc, tableContent, readerTabInfo.GetString(3), ParagraphAlignment.CENTER, 50));
                            tableContent.GetRow(i).GetCell(5).SetParagraph(SetCellText(doc, tableContent, readerTabInfo.GetString(4), ParagraphAlignment.CENTER, 50));
                        }
                        i++;
                    }

                    #endregion
                }
                doc.CreateParagraph();
            }

            #endregion

            #region 保存word文件
            string saveName = $"{cmb_DBList.Text}_数据库文档_{DateTime.Now.ToString("yyyy-MM-dd")}.doc";

            string path = Path.Combine(txt_SaveWordPath.Text, saveName);

            FileStream sw = File.Create(path);

            doc.Write(sw);

            sw.Close();

            if (MessageBox.Show("保存成功，是否打开文件", "提示消息", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                Process.Start(path);
            }
            #endregion
        }

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

        #region 导出HTML文件
        private void btn_SaveHTML_Click(object sender, EventArgs e)
        {

            if (string.IsNullOrEmpty(cmb_DBList.Text))
            {
                MessageBox.Show("请选择数据库");
                return;
            }

            if (string.IsNullOrEmpty(txt_SaveHtmlPath.Text))
            {
                MessageBox.Show("HTML导出路径不能为空");
                return;
            }

            #region 获取数据

            StringBuilder str = new StringBuilder();
            str.Append("<div align=\"center\">");
            str.Append("<div>");
            str.Append($"<h1>{cmb_DBList.Text} 数据库文档</h1>");
            str.Append("</div>");

            string conn = $"data source={txt_dataSource.Text};database={cmb_DBList.Text};PORT={txt_PORT.Text};uid={txt_uid.Text};pwd={txt_pwd.Text};";

            string strSql = $"SELECT table_name,table_comment FROM information_schema.tables WHERE table_schema = '{cmb_DBList.Text}' ";

            var reader = MySqlHelper.ExecuteReader(conn, CommandType.Text, strSql);
            int n = 1;
            while (reader.Read())
            {
                if (reader.HasRows)
                {
                    string tableName = reader.GetString(0);

                    //#region 检验要素列表部分(数据库读取循环显示)

                    string strSqlCount = $@"select COUNT(*) AS NUM
from information_schema.columns where table_schema = '{cmb_DBList.Text}' and table_name = '{tableName}'";

                    var count = MySqlHelper.ExecuteScalar(conn, CommandType.Text, strSqlCount);

                    string strSql1 = $@"select column_name,column_type,IS_NULLABLE,COLUMN_KEY,COLUMN_COMMENT
from information_schema.columns where table_schema = '{cmb_DBList.Text}' and table_name = '{tableName}'
order by ORDINAL_POSITION asc";

                    var readerTabInfo = MySqlHelper.ExecuteReader(conn, CommandType.Text, strSql1);

                    str.Append("<div>");
                    str.Append("<table width=\"80%\" border=\"1\" cellpadding=\"0\" cellspacing=\"0\">");
                    str.Append("<tr>");
                    str.Append($"<td colspan=\"6\" align=\"center\" style=\"color: red; font - size:30px;\"><b> 表名({ tableName})</b></td>");
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

                    int i = 2;
                    while (readerTabInfo.Read())
                    {
                        if (readerTabInfo.HasRows)
                        {

                            str.Append("<tr align=\"center\">");
                            str.Append($"<td>{(i - 1).ToString()}</td>");
                            str.Append($"<td>{readerTabInfo.GetString(0)}</td>");
                            str.Append($"<td>{readerTabInfo.GetString(1)}</td>");
                            str.Append($"<td>{readerTabInfo.GetString(2)}</td>");
                            str.Append($"<td>{readerTabInfo.GetString(3)}</td>");
                            str.Append($"<td>{readerTabInfo.GetString(4)}</td>");
                            str.Append("</tr>");
                        }
                        i++;
                    }
                    str.Append("</table>");
                    #endregion
                }
                str.Append("<br/><br/>");
            }

            #endregion

            str.Append("</div>");

            #region 保存 HTML 文件

            string saveName = $"{cmb_DBList.Text}_数据库文档_{DateTime.Now.ToString("yyyy-MM-dd")}.html";

            string path = Path.Combine(txt_SaveHtmlPath.Text, saveName);

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
    }
}
