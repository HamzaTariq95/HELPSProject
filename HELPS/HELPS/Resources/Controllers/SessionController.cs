using System;
using System.Collections.Generic;
using System.Web.Http;
using UTS.HELPS.WebServices.DataAccess;
using UTS.HELPS.WebServices.DataObjects.Requests;
using UTS.HELPS.WebServices.WebAPI.Constants;
using UTS.HELPS.WebServices.WebAPI.Responses;

namespace UTS.HELPS.WebServices.WebAPI.Controllers
{
    public class SessionController : BaseController
    {
        [HttpGet]
        [Route("api/session/sessionTypes/{active?}")]
        public SessionTypeListResponse ListSessionTypes(bool? active = null)
        {
            try
            {
                base.CheckApplicationKey();

                List<SessionType> list = SessionDb.ListSessionTypes(active);
                return new SessionTypeListResponse()
                {
                    IsSuccess = true,
                    Results = list
                };
            }
            catch (Exception e)
            {
                string msg = CreateExceptionMessage(e);
                return new SessionTypeListResponse()
                {
                    IsSuccess = false,
                    DisplayMessage = string.Format(ErrorMessages.LIST_ERROR, msg)
                };
            }
        }

        [HttpGet]
        [Route("api/session/booking/search")]
        public SessionBookingListResponse SearchSessionBookings([FromUri] SessionBookingSearch bookingSearch)
        {
            try
            {
                base.CheckApplicationKey();

                if (bookingSearch == null)
                {
                    return new SessionBookingListResponse()
                    {
                        IsSuccess = false,
                        DisplayMessage = ErrorMessages.SEARCH_CRITERIA_REQ_ERROR
                    };
                }

                List<SessionBooking> list = SessionDb.SearchSessionBookings(bookingSearch);
                return new SessionBookingListResponse()
                {
                    IsSuccess = true,
                    Results = list
                };
            }
            catch (Exception e)
            {
                string msg = CreateExceptionMessage(e);
                return new SessionBookingListResponse()
                {
                    IsSuccess = false,
                    DisplayMessage = msg
                };
            }
        }

        [HttpPut]
        [Route("api/session/booking/update")]
        public Response UpdateSessionBooking(UpdateSessionBooking update)
        {
            try
            {
                base.CheckApplicationKey();

                if (update == null)
                {
                    return new Response()
                    {
                        IsSuccess = false,
                        DisplayMessage = ErrorMessages.UPDATE_REQ_ERROR
                    };
                }

                SessionDb.UpdateSessionBooking(update);
            }
            catch (Exception e)
            {
                string msg = CreateExceptionMessage(e);
                return new Response()
                {
                    IsSuccess = false,
                    DisplayMessage = string.Format(ErrorMessages.UPDATE_SESSION_BOOKING_ERROR, msg)
                };
            }

            return new Response()
            {
                IsSuccess = true
            };
        }
    }
}
