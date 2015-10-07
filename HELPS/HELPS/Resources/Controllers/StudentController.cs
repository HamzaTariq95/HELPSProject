using System;
using System.Web.Http;
using UTS.HELPS.WebServices.DataAccess;
using UTS.HELPS.WebServices.DataObjects.Requests;
using UTS.HELPS.WebServices.WebAPI.Constants;
using UTS.HELPS.WebServices.WebAPI.Responses;

namespace UTS.HELPS.WebServices.WebAPI.Controllers
{
    public class StudentController : BaseController
    {
        [HttpPost]
        [Route("api/student/register")]
        public Response RegisterStudent(StudentReg studentReg)
        {
            try
            {
                base.CheckApplicationKey();

                StudentDb.RegisterStudent(studentReg);
            }
            catch (Exception e)
            {
                string msg = CreateExceptionMessage(e);
                return new Response()
                {
                    IsSuccess = false,
                    DisplayMessage = string.Format(ErrorMessages.STUDENT_REGISTER_ERROR, msg)
                };
            }

            return new Response()
            {
                IsSuccess = true
            };
        }
    }
}
