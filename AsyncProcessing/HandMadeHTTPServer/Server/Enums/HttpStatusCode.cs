namespace MyCoolWebServer.Server.Enums
{
    public enum HttpStatusCode //there is a class in System.Net for this
    {
        Ok = 200,
       	MovedPermanently = 301,
    	Found = 302,
    	MovedTemporarily = 303,
    	NotAuthorized = 401,
    	NotFound = 404,
    	InternalServerError = 500
    }
}
