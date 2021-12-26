using DevExpress.Data.Utils;
using DevExpress.LookAndFeel;
using DevExpress.Skins;
using DevExpress.Utils;
using DevExpress.Utils.Frames;
using DevExpress.XtraBars;
using DevExpress.XtraBars.Helpers;
using DevExpress.XtraBars.Ribbon;
using DevExpress.XtraBars.Ribbon.ViewInfo;
using DevExpress.XtraPdfViewer;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Windows.Forms;

namespace HfutIe
{
    public partial class MyFilesView : RibbonForm
    {
        const string glyphImagePath = "{0}_16x16.png";
        const string largeGlyphImagePath = "{0}_32x32.png";

        public GUIDFILES current_file;//当前显示的文件
        public List<GUIDFILES> list_document = new List<GUIDFILES>();//作业指示文档

        readonly PopupMenu popupSkins = new PopupMenu();
        readonly string mainFormText;
        public string materialCode = "";
        public MyFilesView()
        {
            InitializeComponent();
            pdfViewer.DocumentCreator = "PDF Viewer Demo";
            pdfViewer.DocumentProducer = "Developer Express Inc., " + AssemblyInfo.Version;
            UserLookAndFeel.Default.SetSkinStyle("Office 2013");
            //pdfViewer.CreateRibbon();
            //foreach (RibbonPage page in ribbonControl.Pages)
            //    if (page.Text == "图纸浏览")
            //    {
            //        popupSkins.BeginUpdate();
            //        popupSkins.Ribbon = ribbonControl;
            //        checkItemAllowFormSkins = new BarCheckItem(ribbonControl.Manager);
            //        checkItemAllowFormSkins.Caption = "Allow Form Skins";
            //        checkItemAllowFormSkins.ItemClick += new ItemClickEventHandler(OnAllowFormSkinsItemClick);
            //        popupSkins.AddItem(checkItemAllowFormSkins);
            //        SkinHelper.InitSkinPopupMenu(popupSkins);
            //        popupSkins.ItemLinks[1].BeginGroup = true;
            //        popupSkins.EndUpdate();
            //        popupSkins.Popup += new EventHandler(OnPmSkinsPopup);
            //        RibbonPageGroup skinsPage = new RibbonPageGroup(ribbonGallerySkins.Caption);
            //        SkinHelper.InitSkinGallery(ribbonGallerySkins, true);
            //        skinsPage.CaptionButtonClick += new RibbonPageGroupEventHandler(OnSkinsPageCaptionButtonClick);
            //        skinsPage.ItemLinks.Add(ribbonGallerySkins);
            //        RibbonPageGroup devExpressPage = new RibbonPageGroup("DevExpress");
            //        devExpressPage.ShowCaptionButton = false;
            //        AddBarItem(devExpressPage, "Getting Started", "GetStarted", OnGettingStartedItemClicked);
            //        AddBarItem(devExpressPage, "About", "Info", OnAboutItemClicked);
            //        page.Groups.AddRange(new RibbonPageGroup[] { skinsPage, devExpressPage });
            //        break;
            //    }
            mainFormText = Text;
            pdfViewer.DocumentChanged += new PdfDocumentChangedEventHandler(OnPdfViewerDocumentChanged);
            pdfViewer.UriOpening += OnPdfViewerUriOpening;
        }
        public string filePath = "";

        /// <summary>
        /// 加载PDF文件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MyFilesView_Load(object sender, EventArgs e)
        {
            if (current_file != null)
            {
                this.be_filenum_all.EditValue = list_document.Count;
                this.be_filenum_current.EditValue =1;
                Stream ms = null;
                ms = new MemoryStream(current_file.document_file);
                showLoading("文档加载中...");

                //string file_type = current_file.document_type;
                string file_type = current_file.document_type;
                string fileName= current_file.document_name;
                switch (file_type)
                {
                    case "pdf":
                        this.guidfile_pcl.Controls.Clear();
                        //PdfViewer pdfViewer = new PdfViewer(); pdfViewer.CreateRibbon();
                        pdfViewer.Name = "pdfViewer";
                        pdfViewer.LoadDocument(ms);
                        pdfViewer.NavigationPaneVisibility = DevExpress.XtraPdfViewer.PdfNavigationPaneVisibility.Hidden;
                        pdfViewer.Dock = DockStyle.Fill;
                        pdfViewer.Cursor = Cursors.Hand;
                        pdfViewer.ZoomMode = DevExpress.XtraPdfViewer.PdfZoomMode.FitToVisible;
                        //pdfViewer.DocumentChanged += new PdfDocumentChangedEventHandler(OnPdfViewerDocumentChanged);
                        //pdfViewer.UriOpening += OnPdfViewerUriOpening;
                        this.guidfile_pcl.Controls.Add(pdfViewer);
                        if (String.IsNullOrEmpty(fileName))
                        {
                            Text = mainFormText;
                        }
                        else
                        {
                            Text = fileName + " - " + mainFormText;
                        }
                        break;
                    case "jpg":
                    case "jepg":
                    case "png":
                    case "gif":
                        this.guidfile_pcl.Controls.Clear();
                        Bitmap bmpt = new Bitmap(ms);
                        //PictureBox pic = new PictureBox();
                        pic.Name = "PICTURE";
                        pic.Dock = DockStyle.Fill;
                        pic.Cursor = Cursors.Hand;
                        //PB.ErrorImage = .;
                        pic.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
                        pic.Image = bmpt;
                        this.guidfile_pcl.Controls.Add(pic);
                        if (String.IsNullOrEmpty(fileName))
                        {
                            Text = mainFormText;
                        }
                        else
                        {
                            Text = fileName + " - " + mainFormText;
                        }
                        break;
                    default:
                        MessageBox.Show("您选择的文档格式暂不支持！", "系统提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                        break;
                }
                closeLoading();
            }
            else
            {
                this.be_filenum_all.EditValue = list_document.Count;
                this.be_filenum_current.EditValue =0;
                this.guidfile_pcl.Controls.Clear();
            }
        }
        private void MyFilesView_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                this.pdfViewer.Dispose();
            }
            catch (Exception)
            {
            }
        }

        ///// <summary> 
        ///// 从文件读取 Stream 
        ///// </summary> 
        //public Stream FileToStream(string fileName)
        //{
        //    // 打开文件 
        //    FileStream fileStream = new FileStream(fileName, FileMode.Open, FileAccess.Read, System.IO.FileShare.Read);
        //    // 读取文件的 byte[] 
        //    byte[] bytes = new byte[fileStream.Length];
        //    fileStream.Read(bytes, 0, bytes.Length);
        //    fileStream.Close();
        //    // 把 byte[] 转换成 Stream 
        //    Stream stream = new MemoryStream(bytes);
        //    return stream;
        //}

        ///// <summary>
        ///// 打开
        ///// </summary>
        ///// <param name="sender"></param>
        ///// <param name="e"></param>
        //private void btn_open_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        //{
        //    OpenFileDialog fileDialog = new OpenFileDialog();
        //    //fileDialog.Filter = "Excel文件(*.xls;*.xlsx)|*.xls;*.xlsx|所有文件|*.*";
        //    fileDialog.Filter = "所有文件|*.*";
        //    fileDialog.ValidateNames = true;
        //    fileDialog.CheckPathExists = true;
        //    fileDialog.CheckFileExists = true;
        //    if (fileDialog.ShowDialog() == DialogResult.OK)
        //    {
        //        showLoading("正在加载文件....");
        //        string strFileName = fileDialog.FileName;
        //        string file_type = strFileName.Substring(strFileName.LastIndexOf(".") + 1).ToLower();
        //        switch (file_type)
        //        {
        //            case "pdf":
        //                this.pdfViewer.DocumentFilePath = strFileName;
        //                break;
        //            default:
        //                MessageBox.Show("暂不支持打开其他格式文件\n【目前只支持打开PDF文档】", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        //                break;
        //        }
        //    }
        //}

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);
            if (pdfViewer.IsDocumentChanged)
                e.Cancel = !pdfViewer.ShowDocumentClosingWarning();
        }
        void OnPdfViewerUriOpening(object sender, PdfUriOpeningEventArgs e)
        {
            e.Handled = String.Compare(e.Uri.AbsoluteUri, AssemblyInfo.DXLinkGetStarted, true) == 0;
        }
        void OnPdfViewerDocumentChanged(object sender, PdfDocumentChangedEventArgs e)
        {
            string fileName = Path.GetFileName(e.DocumentFilePath);
            pdfViewer.ZoomMode = DevExpress.XtraPdfViewer.PdfZoomMode.FitToVisible;
            if (String.IsNullOrEmpty(fileName))
            {
                Text = mainFormText;
            }
            else
            {
                Text = fileName + " - " + mainFormText;
            }
        }

        /// <summary>
        /// 打开提示框
        /// </summary>
        /// <param name="showString"></param>
        private void showLoading(string showString)
        {
            DevExpress.XtraSplashScreen.SplashScreenManager.ShowDefaultWaitForm("系统提示", showString);
        }

        /// <summary>
        /// 关闭提示框
        /// </summary>
        private void closeLoading()
        {
            try
            {
                //DevExpress.XtraSplashScreen.SplashScreenManager.ShowDefaultWaitForm("系统提示", "正在查询....");
                DevExpress.XtraSplashScreen.SplashScreenManager.CloseDefaultWaitForm();
            }
            catch (Exception eer)
            {
                //Alert.ShowAlertControlErr(new Form(), "系统提示", "关闭提示框错误：" + eer.Message, 2000);
            }
        }

        private void btn_privious_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (list_document != null && list_document.Count >= 1)
            {
                int document_current_num = Convert.ToInt32(be_filenum_current.EditValue) - 1;
                if (document_current_num >= 0)
                {
                    try
                    {
                        this.guidfile_pcl.Controls.Clear();
                        MemoryStream ms;
                        string file_type = "";
                        string fileName = "";
                        if (document_current_num ==0)
                        {
                            current_file = list_document[document_current_num];
                            ms = new MemoryStream(list_document[document_current_num].document_file);
                            showLoading("文档加载中...");
                            file_type = list_document[document_current_num].document_type;
                            fileName = list_document[document_current_num].document_name;
                            document_current_num = document_current_num + 1;
                        }
                        else
                        {
                            current_file = list_document[document_current_num - 1];
                            ms = new MemoryStream(list_document[document_current_num - 1].document_file);
                            showLoading("文档加载中...");
                            file_type = list_document[document_current_num - 1].document_type;
                            fileName = list_document[document_current_num - 1].document_name;
                        }
                        switch (file_type)
                        {
                            case "pdf":
                                pdfViewer.Name = "pdfViewer";
                                pdfViewer.LoadDocument(ms);
                                pdfViewer.NavigationPaneVisibility = DevExpress.XtraPdfViewer.PdfNavigationPaneVisibility.Hidden;
                                pdfViewer.Dock = DockStyle.Fill;
                                pdfViewer.Cursor = Cursors.Hand;
                                pdfViewer.ZoomMode = DevExpress.XtraPdfViewer.PdfZoomMode.FitToVisible;
                                this.guidfile_pcl.Controls.Add(pdfViewer);
                                be_filenum_current.EditValue = document_current_num + "";

                                pdfPreviousPageBarItem1.Enabled = true;
                                pdfNextPageBarItem1.Enabled = true;
                                pdfFindTextBarItem1.Enabled = true;
                                pdfZoomInBarItem1.Enabled = true;
                                pdfZoomOutBarItem1.Enabled = true;
                                pdfExactZoomListBarSubItem1.Enabled = true;
                                pdfFilePrintBarItem1.Enabled = true;
                                pdfFileSaveAsBarItem1.Enabled = true;

                                if (String.IsNullOrEmpty(fileName))
                                {
                                    Text = mainFormText;
                                }
                                else
                                {
                                    Text = fileName + " - " + mainFormText;
                                }
                                break;
                            case "jpg":
                            case "jepg":
                            case "png":
                            case "gif":
                                this.guidfile_pcl.Controls.Clear();
                                Bitmap bmpt = new Bitmap(ms);
                                //PictureBox pic = new PictureBox();
                                pic.Name = "PICTURE";
                                pic.Dock = DockStyle.Fill;
                                pic.Cursor = Cursors.Hand;
                                //PB.ErrorImage = .;
                                pic.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
                                pic.Image = bmpt;
                                this.guidfile_pcl.Controls.Add(pic);
                                be_filenum_current.EditValue = document_current_num + "";

                                if (String.IsNullOrEmpty(fileName))
                                {
                                    Text = mainFormText;
                                }
                                else
                                {
                                    Text = fileName + " - " + mainFormText;
                                }

                                pdfPreviousPageBarItem1.Enabled = false;
                                pdfNextPageBarItem1.Enabled = false;
                                pdfFindTextBarItem1.Enabled = false;
                                pdfZoomInBarItem1.Enabled = false;
                                pdfZoomOutBarItem1.Enabled = false;
                                pdfExactZoomListBarSubItem1.Enabled = false;
                                pdfFilePrintBarItem1.Enabled = false;
                                pdfFileSaveAsBarItem1.Enabled = false;

                                break;
                            default:
                                MessageBox.Show("您选择的文档格式暂不支持！", "系统提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                                break;
                        }
                        closeLoading();
                    }
                    catch (Exception err)
                    {
                        MessageBox.Show("指示文档显示Err:" + err.Message, "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void btn_next_ItemClick(object sender, ItemClickEventArgs e)
        {

            if (list_document != null && list_document.Count >= 1)
            {
                int document_current_num = Convert.ToInt32(be_filenum_current.EditValue) - 1;
                if (document_current_num <=(list_document.Count - 1))
                {
                    try
                    {
                        this.guidfile_pcl.Controls.Clear();
                        MemoryStream ms;
                        string file_type = "";
                        string fileName = "";
                        if (document_current_num == list_document.Count - 1)
                        {
                            current_file = list_document[document_current_num];
                            ms = new MemoryStream(list_document[document_current_num ].document_file);
                            showLoading("文档加载中...");
                            file_type = list_document[document_current_num ].document_type;
                            fileName = list_document[document_current_num ].document_name;
                            document_current_num = document_current_num - 1;
                        }
                        else
                        {
                            current_file = list_document[document_current_num + 1];
                             ms = new MemoryStream(list_document[document_current_num + 1].document_file);
                            showLoading("文档加载中...");
                            file_type = list_document[document_current_num + 1].document_type;
                            fileName = list_document[document_current_num + 1].document_name;
                        }
                        switch (file_type)
                        {
                            case "pdf":
                                //PdfViewer pdfViewer = new PdfViewer(); pdfViewer.CreateRibbon();
                                pdfViewer.Name = "pdfViewer";
                                pdfViewer.LoadDocument(ms);
                                pdfViewer.NavigationPaneVisibility = DevExpress.XtraPdfViewer.PdfNavigationPaneVisibility.Hidden;
                                pdfViewer.Dock = DockStyle.Fill;
                                pdfViewer.Cursor = Cursors.Hand;
                                pdfViewer.ZoomMode = DevExpress.XtraPdfViewer.PdfZoomMode.FitToVisible;
                                //pdfViewer.DocumentChanged += new PdfDocumentChangedEventHandler(OnPdfViewerDocumentChanged);
                                //pdfViewer.UriOpening += OnPdfViewerUriOpening;
                                this.guidfile_pcl.Controls.Add(pdfViewer);
                                document_current_num = document_current_num + 2;
                                be_filenum_current.EditValue = document_current_num + "";

                                pdfPreviousPageBarItem1.Enabled = true;
                                pdfNextPageBarItem1.Enabled = true;
                                pdfFindTextBarItem1.Enabled = true;
                                pdfZoomInBarItem1.Enabled = true;
                                pdfZoomOutBarItem1.Enabled = true;
                                pdfExactZoomListBarSubItem1.Enabled = true;
                                pdfFilePrintBarItem1.Enabled = true;
                                pdfFileSaveAsBarItem1.Enabled = true;
                                if (String.IsNullOrEmpty(fileName))
                                {
                                    Text = mainFormText;
                                }
                                else
                                {
                                    Text = fileName + " - " + mainFormText;
                                }
                                break;
                            case "jpg":
                            case "jepg":
                            case "png":
                            case "gif":
                                //this.guidfile_pcl.Controls.Clear();
                                Bitmap bmpt = new Bitmap(ms);
                                //PictureBox pic = new PictureBox();
                                pic.Name = "PICTURE";
                                pic.Dock = DockStyle.Fill;
                                pic.Cursor = Cursors.Hand;
                                //PB.ErrorImage = .;
                                pic.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
                                pic.Image = bmpt;
                                this.guidfile_pcl.Controls.Add(pic);
                                document_current_num = document_current_num + 2;
                                be_filenum_current.EditValue = document_current_num + "";

                                if (String.IsNullOrEmpty(fileName))
                                {
                                    Text = mainFormText;
                                }
                                else
                                {
                                    Text = fileName + " - " + mainFormText;
                                }

                                pdfPreviousPageBarItem1.Enabled = false;
                                pdfNextPageBarItem1.Enabled = false;
                                pdfFindTextBarItem1.Enabled = false;
                                pdfZoomInBarItem1.Enabled = false;
                                pdfZoomOutBarItem1.Enabled = false;
                                pdfExactZoomListBarSubItem1.Enabled = false;
                                pdfFilePrintBarItem1.Enabled = false;
                                pdfFileSaveAsBarItem1.Enabled = false;
                                break;
                            default:
                                MessageBox.Show("您选择的文档格式暂不支持！", "系统提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                                break;
                        }
                        closeLoading();
                    }
                    catch (Exception err)
                    {
                        MessageBox.Show("指示文档显示Err:" + err.Message, "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void pdfFileOpenBarItem1_ItemClick(object sender, ItemClickEventArgs e)
        {
            this.guidfile_pcl.Controls.Remove(pic);
            pdfViewer.CloseDocument();
            pdfViewer.Name = "pdfViewer";
            //this.pdfViewer.DocumentFilePath = strFileName;
            pdfViewer.NavigationPaneVisibility = DevExpress.XtraPdfViewer.PdfNavigationPaneVisibility.Hidden;
            pdfViewer.Dock = DockStyle.Fill;
            pdfViewer.Cursor = Cursors.Hand;
            pdfViewer.ZoomMode = DevExpress.XtraPdfViewer.PdfZoomMode.FitToVisible;
            this.guidfile_pcl.Controls.Add(pdfViewer);
        } 
    }
}
