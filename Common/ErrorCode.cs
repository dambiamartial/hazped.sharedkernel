namespace hazped.sharedkernel.Common;

public enum ErrorCode
{
    NOT_FOUND = 404,
    Conflict = 409,
    SERVER_ERROR = 500,

    //Validation errors should be in the range 100 - 199
    VALIDATION_ERROR = 101,
    ENTITY_STATE_ERROR = 102,

    //Infrastructure errors should be in the range 200-299
    DB_UPDATE_ERROR = 200,

    //Application errors should be in the range 300 - 399

    UnknownError = 999
}
