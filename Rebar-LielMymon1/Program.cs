
using MongoDataAccess.DataAccess;
using Rebar_LielMymon1;
using MongoDataAccess.Models;

RebarDataAccess db= new RebarDataAccess();

  RebarBranchModel Rebarbranch=new RebarBranchModel() {ManagerName="liel",ManagerPassword="12345",Address="somewhere" };
  await db.AddNewRebarBranch(Rebarbranch);


await db.WhatsYourOrder();
await db.CashBoxClose(Rebarbranch);

//Adding new Shake:
//await db.AddNewShake(new ShakeModel() { Name = "XYZ", Dis = "IM A SHAKE", Price_L = 20, Price_M = 15, Price_S = 13 });






