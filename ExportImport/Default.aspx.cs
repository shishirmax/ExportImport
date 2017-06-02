using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ExportImport
{
    public partial class _Default : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                populateDatabaseData();
                lblMessage.Text = "Current Database Data.";
            }
        }

        private void populateDatabaseData()
        {
            using (TestDBEntities dc = new TestDBEntities())
            {
                gvData.DataSource = dc.MTS.ToList();
                gvData.DataBind();
            }
        }
        

        protected void btnImportFromCSV_Click(object sender, EventArgs e)
        {
            //var regex = new Regex("\\\"(.*?)\\\"");

            if (FileUpload1.PostedFile.ContentType == "text/csv"|| FileUpload1.PostedFile.ContentType == "application/vnd.ms-excel")
            {
                string fileName = Path.Combine(Server.MapPath("~/UploadDocument"), Guid.NewGuid().ToString() + ".csv");
                try
                {
                    FileUpload1.PostedFile.SaveAs(fileName);

                    string[] Lines = File.ReadAllLines(fileName);
                    string[] Fields;

                    //Remove Header Line
                    Lines = Lines.Skip(1).ToArray();
                   // Lines = regex.Replace(Lines, m => m.Value.Replace(',', ' '));
                    List<MT> emList = new List<MT>();
                    foreach(var line in Lines)
                    {
                        //Fields = line.Split(new char[] { ',' });
                        Fields = Regex.Split(line, "\\\"(.*?)\\\"");
                        emList.Add(
                            new MT
                            {
                                
                                DatePosted = Convert.ToDateTime(Fields[0]),
                                TransactionReference = Fields[0].Replace("\"", ""),
                                AttorneyDocket = Fields[2].Replace("\"", ""),
                                Status = Fields[3].Replace("\"", ""),
                                TransactionID = Fields[4].Replace("\"", ""),
                                Type = Fields[5].Replace("\"", ""),
                                //TotalPaymentRefund = regex.Replace(Fields[6].Replace("\"", ""),m=> m.Value.Replace(',',' ')),
                                TotalPaymentRefund = Fields[6].Replace("\"", ""),
                                CustomerName = Fields[7]
                            });
                    }

                    //update database data
                    using (TestDBEntities dc = new TestDBEntities())
                    {
                        foreach(var i in emList)
                        {
                            var v = dc.MTS.Where(a => a.CSVReferenceNumber.Equals(i.CSVReferenceNumber)).FirstOrDefault();
                            if(v != null)
                            {
                                v.DatePosted = i.DatePosted;
                                v.TransactionReference = i.TransactionReference;
                                v.AttorneyDocket = i.AttorneyDocket;
                                v.Status = i.Status;
                                v.TransactionID = i.TransactionID;
                                v.Type = i.Type;
                                v.TotalPaymentRefund = i.TotalPaymentRefund;
                                v.CustomerName = i.CustomerName;
                            }
                            else
                            {
                                dc.MTS.Add(i);
                            }
                        }
                        dc.SaveChanges();

                        //populate updated data
                        populateDatabaseData();
                        lblMessage.Text = "Sucessfully Imported.";
                    }

                }
                catch(Exception)
                {
                    throw;
                }
            }
        }
    }
}