var GigsController = function (attendanceService) {
    var button;

    var init = function (container) {
        $(container).on('click', '.js-toggle-attendance', toggleAttendance);
    };

    var toggleAttendance = function (e) {
        button = $(e.target);
        var gigId = button.attr("data-gig-id");
        button.hasClass('btn-default') ?
            attendanceService.attend(gigId, success, error) :
            attendanceService.cancelAttendance(gigId, success, error);
    };

    var success = function () {
        var text = button.text() === 'Going' ? 'Going ?' : 'Going';
        button.toggleClass('btn-info').toggleClass('btn-default').text(text);
    };

    var error = function () {
        alert("Something failed!");
    };

    return {
        init: init
    };
}(AttendanceService);