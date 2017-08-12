var FollowingService = function () {
    var follow = function (followeeId, success, error) {
        $.ajax({
            url: "/api/followings/follow",
            method: "POST",
            data: JSON.stringify({ "FolloweeId": followeeId }),
            contentType: "application/json; charset=utf-8",
            success: success,
            error: error
        });
    };

    var unfollow = function (followeeId, success, error) {
        $.ajax({
            url: "/api/followings/" + followeeId,
            method: "DELETE",
            success: success,
            error: error
        });
    };

    return {
        follow: follow,
        unfollow: unfollow
    };
}();