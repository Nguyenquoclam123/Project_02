using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace BTVN_02
{
    public partial class Form1 : Form
    {
        private TreeNode selectedNode;
        private XmlDocument xmlDoc;
        private OpenFileDialog openFileDialog1;
        public Form1()
        {
            InitializeComponent();
            openFileDialog1 = new OpenFileDialog();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                xmlDoc = new XmlDocument();
                xmlDoc.Load(openFileDialog1.FileName);
                treeView1.Nodes.Clear();
                treeView1.Nodes.Add(new TreeNode(xmlDoc.DocumentElement.Name));
                TreeNode tNode = new TreeNode();
                tNode = (TreeNode)treeView1.Nodes[0];
                addTreeNode(xmlDoc.DocumentElement, tNode);
                treeView1.ExpandAll();
            }
        }

        private void addTreeNode(XmlNode xmlNode, TreeNode treeNode)
        {
            XmlNode xNode;
            TreeNode tNode;
            XmlNodeList xNodeList;
            if (xmlNode.HasChildNodes) //The current node has children
            {
                xNodeList = xmlNode.ChildNodes;
                for (int x = 0; x <= xNodeList.Count - 1; x++) //Loop through the child nodes
                {
                    xNode = xmlNode.ChildNodes[x];
                    tNode = treeNode.Nodes.Add(xNode.Name);
                    tNode.Tag = xNode;
                    if (xNode.HasChildNodes) //The current child node has children
                    {
                        addTreeNode(xNode, tNode);
                    }
                }
            }
            else //The current node does not have children
            {
                treeNode.Nodes.Add(xmlNode.InnerText);
            }
        }
        private void button2_Click(object sender, EventArgs e)
        {
            if (treeView1.SelectedNode != null)
            {
                XmlNode xNode = (XmlNode)treeView1.SelectedNode.Tag;
                SaveFileDialog saveFileDialog1 = new SaveFileDialog();
                saveFileDialog1.Filter = "XML files (*.xml)|*.xml";
                saveFileDialog1.FilterIndex = 2;
                saveFileDialog1.RestoreDirectory = true;
                if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    XmlDocument xmlDoc = new XmlDocument();
                    xmlDoc.LoadXml(xNode.OwnerDocument.OuterXml);
                    xmlDoc.Save(saveFileDialog1.FileName);
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (treeView1.SelectedNode != null)
            {
                XmlNode xNode = (XmlNode)treeView1.SelectedNode.Tag;
                MessageBox.Show(xNode.InnerText);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (treeView1.SelectedNode != null)
            {
                XmlNode xNode = (XmlNode)treeView1.SelectedNode.Tag;
                InputBox ib = new InputBox();
                ib.ShowDialog();
                if (ib.DialogResult == DialogResult.OK)
                {
                    xNode.InnerText = ib.input;
                }
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (treeView1.SelectedNode != null)
            {
                treeView1.SelectedNode.Remove();
            }
        }
    }

    public class InputBox : Form
    {
        private TextBox textBox1;
        private Button button1;
        private Button button2;

        public InputBox()
        {
            this.textBox1 = new TextBox();
            this.button1 = new Button();
            this.button2 = new Button();
            this.SuspendLayout();

            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.button2);

            //
            // textBox1
            //
            this.textBox1.Location = new System.Drawing.Point(13, 13);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(100, 20);
            this.textBox1.TabIndex = 0;

            //
            // button1
            //
            this.button1.Location = new System.Drawing.Point(119, 12);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 1;
            this.button1.Text = "OK";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);

            //
            // button2
            //
            this.button2.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.button2.Location = new System.Drawing.Point(199, 12);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 2;
            this.button2.Text = "Cancel";
            this.button2.UseVisualStyleBackColor = true;

            //
            // InputBox
            //
            this.AcceptButton = this.button1;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.button2;
            this.ClientSize = new System.Drawing.Size(284, 55);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.textBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "InputBox";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Input";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        public string input
        {
            get { return this.textBox1.Text; }
            set { this.textBox1.Text = value; }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
