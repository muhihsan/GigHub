var AttendanceService = function () {
    var attend = function (gigId, success, error) {
        $.ajax({
            url: "/api/attendances/attend",
            method: "POST",
            data: JSON.stringify({ "GigId": gigId }),
            contentType: "application/json; charset=utf-8",
            success: success,
            error: error
        });
    };

    var cancelAttendance = function (gigId, success, error) {
        $.ajax({
            url: "/api/attendances/" + gigId,
            method: "DELETE",
            success: success,
            error: error
        });
    };

    return {
        attend: attend,
        cancelAttendance: cancelAttendance
    };
}();