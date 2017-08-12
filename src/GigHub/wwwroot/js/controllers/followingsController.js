var FollowingsController = function (followingService) {
    var button;

    var init = function (container) {
        $(container).on('click', '.js-toggle-follow', toggleFollowing);
    };

    var toggleFollowing = function (e) {
        button = $(e.target);
        var followId = button.attr("data-user-id");
        button.hasClass('btn-default') ?
            followingService.follow(followId, success, error) :
            followingService.unfollow(followId, success, error);
    };

    var success = function () {
        var text = button.text() === 'Following' ? 'Follow' : 'Following';
         button.toggleClass('btn-info').toggleClass('btn-default').text(text);
    };

    var error = function () {
        alert("Something failed!");
    };

    return {
        init: init
    };
}(FollowingService);