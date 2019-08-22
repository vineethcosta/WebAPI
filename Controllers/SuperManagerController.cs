using BankAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace BankAPI.Controllers
{
    public class SuperManagerController : ApiController
    {
        OnlineBankEntities3 dbContext = new OnlineBankEntities3();
        [Route("api/SuperManager/getAllManagers")]
        [HttpGet]
        public HttpResponseMessage getAllManagers()
        {
            List<userDetail> managers = new List<userDetail>();
            try
            {
               managers = dbContext.userDetails.Where(val=>val.managerId == 999).ToList();
            }
            catch (Exception e)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, e);
            }

            return Request.CreateResponse(HttpStatusCode.OK, managers);
        }
        [HttpGet]
        [Route("api/SuperManager/getSingleManager")]
        public HttpResponseMessage getSingleManager(int managerId)
        {
            userDetail singleManager = new userDetail();
            try
            {
                singleManager = dbContext.userDetails.Where(x => x.userId == managerId && x.managerId == 999).Single<userDetail>();
            }
            catch (Exception e)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, e);
            }

            return Request.CreateResponse(HttpStatusCode.OK, singleManager);
        }

        [HttpGet]
        [Route("api/SuperManager/getUnassignedBranch")]
        public HttpResponseMessage getUnassignedBranch()
        {
            List<branchDetail> getUnassignedBranchList=dbContext.branchDetails.Where(val=>val.assigned==0).ToList();
            return Request.CreateResponse(HttpStatusCode.OK,getUnassignedBranchList);
        }


        [HttpPost]
        [Route("api/SuperManager/addManager")]
        public HttpResponseMessage addManager(userDetail addManagerDetails)
        {
            loginDetail addLoginFirst = new loginDetail
            {
                loginId = addManagerDetails.emailId,
                loginPassword = "MTIz",
                userRole = "Manager"

            };
                dbContext.loginDetails.Add(addLoginFirst);

                dbContext.userDetails.Add(addManagerDetails);
                dbContext.SaveChanges();
                changeAssignedValueToOne(addManagerDetails.branchId);
            return Request.CreateResponse(HttpStatusCode.OK, "success");
        }

        [HttpGet]
        [Route("api/SuperManager/getNonAssignedBranches")]
        public HttpResponseMessage getNonAssignedBranches()
        {
            List<String> nonAssignedBranchIdList = new List<String>();
            try
            {

                List<branchDetail> nonAssignedBranchList = dbContext.branchDetails.Where(val => val.assigned == 0).ToList<branchDetail>();

                foreach (branchDetail s in nonAssignedBranchList)
                {
                    nonAssignedBranchIdList.Add(s.branchId);
                }
            }
            catch (Exception e)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, e);
            }

            return Request.CreateResponse(HttpStatusCode.OK, nonAssignedBranchIdList);
        }

        [HttpPost]
        [Route("api/SuperManager/changeBranch")]
        public HttpResponseMessage changeBranch( string oldBranchId, string newBranchId)
        {

            branchDetail branchManager = dbContext.branchDetails.Where(val => val.branchId == oldBranchId).Single<branchDetail>();
            changeAssignedValueToZero(branchManager.branchId);
            changeAssignedValueToOne(newBranchId);
            dbContext.SaveChanges();
            return Request.CreateResponse(HttpStatusCode.OK, "success");

        }

        [HttpPost]
        [Route("api/SuperManager/editManager")]
        public HttpResponseMessage editManager(userDetail editManagerDetails)
        {

               
                userDetail editManager = dbContext.userDetails.Where(val => val.userId == editManagerDetails.userId).Single<userDetail>();
                editManager.userName = editManagerDetails.userName;
                editManager.gender = editManagerDetails.gender;
                editManager.dateOfBirth = editManagerDetails.dateOfBirth;
                editManager.address = editManagerDetails.address;
                editManager.state = editManagerDetails.state;
                editManager.city = editManagerDetails.city;
                editManager.pincode = editManagerDetails.pincode;
                editManager.branchId = editManagerDetails.branchId;
                editManager.phoneNumber = editManagerDetails.phoneNumber;
                editManager.emailId = editManagerDetails.emailId;
                dbContext.SaveChanges();
            
           

            return Request.CreateResponse(HttpStatusCode.OK, "success");


        }
        [HttpPost]
        [Route("api/SuperManager/deleteManager")]
        public HttpResponseMessage deleteManager(int managerId)
        {
            
            
                
                userDetail deleteManager = dbContext.userDetails.Where(val => val.userId == managerId).Single<userDetail>();
                loginDetail deleteManagerLogin = dbContext.loginDetails.Where(val => val.loginId == deleteManager.emailId).Single<loginDetail>();
                dbContext.loginDetails.Remove(deleteManagerLogin);
                
                dbContext.SaveChanges();
                changeAssignedValueToZero(deleteManager.branchId);

            
            

            return Request.CreateResponse(HttpStatusCode.OK, "success");
        }

        public void changeAssignedValueToZero(string branchId)
        {
            branchDetail setAssigned = dbContext.branchDetails.Where(val => val.branchId == branchId).Single<branchDetail>();
          
                setAssigned.assigned = 0;
            dbContext.SaveChanges();

        }
        public void changeAssignedValueToOne(string branchId)
        {
            branchDetail setAssigned = dbContext.branchDetails.Where(val => val.branchId == branchId).Single<branchDetail>();
          
                setAssigned.assigned = 1;
            dbContext.SaveChanges();

        }
        [HttpGet]
        [Route("api/SuperManager/getBranchId")]
        public HttpResponseMessage getBranchId(int managerId)
        {
            userDetail getBranchId = dbContext.userDetails.Where(val => val.userId == managerId).Single<userDetail>();
            return Request.CreateResponse(HttpStatusCode.OK, getBranchId.branchId); 

        }
        public string getBranch(int managerId)
        {
            userDetail getBranchId = dbContext.userDetails.Where(val => val.userId == managerId).Single<userDetail>();
            return getBranchId.branchId;
        }


    }
}
