<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="ExportImport._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
            <div>
            <h3>Import / Export data from csv.</h3>
            <div>
                <table>
                    <tr>
                        <td>Select File : </td>
                        <td>
                            <asp:FileUpload ID="FileUpload1" runat="server" />
                        </td>
                        <td>
                            <asp:Button ID="btnImportFromCSV" runat="server" Text="Import Data to Database" OnClick="btnImportFromCSV_Click" />
                        </td>
                    </tr>
                </table>
                <div>
                    <br />
                    <asp:Label ID="lblMessage" runat="server" Font-Bold="true" />
                    <br />
                    <asp:GridView ID="gvData" runat="server" AutoGenerateColumns="false">
                        <EmptyDataTemplate>
                            <div style="padding:10px;">No Data Found!</div>
                        </EmptyDataTemplate>
                        <Columns>
                            <asp:BoundField HeaderText="CSVReferenceNumber" DataField="CSVReferenceNumber" />
                            <asp:BoundField HeaderText="DatePosted" DataField="DatePosted" />
                            <asp:BoundField HeaderText="TransactionReference" DataField="TransactionReference" />
                            <asp:BoundField HeaderText="AttorneyDocket" DataField="AttorneyDocket" />
                            <asp:BoundField HeaderText="Status" DataField="Status" />
                            <asp:BoundField HeaderText="TransactionID" DataField="TransactionID" />
                            <asp:BoundField HeaderText="Type" DataField="Type" />
                            <asp:BoundField HeaderText="TotalPaymentRefund" DataField="TotalPaymentRefund" />
                            <asp:BoundField HeaderText="CustomerName" DataField="CustomerName" />
                        </Columns>
                    </asp:GridView>
                    <br />
                    <%--<asp:Button ID="btnExportToCSV" runat="server" Text="Export Data to CSV" OnClick="btnExportToCSV_Click" />--%>
                </div>
            </div>
        </div>    
            

</asp:Content>
