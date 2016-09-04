var navigationNS = navigationNS || {};

navigationNS.Active = function(current, homeName, menuWrapper) {
    this.homeName = homeName;
    this.current = current === "" ? homeName : current;
    this.sideBarLinks = $(menuWrapper);
}
navigationNS.Active.prototype = function() {
    var setCurrent = function() {
        for (var i = 0, link; i < this.sideBarLinks.length; i++) {
            link = this.sideBarLinks[i];
            if (this.current === link.id) {
                $(link).addClass('active');
                break;
            }
        }
    }
    return { setCurrent: setCurrent };
}();

navigationNS.Pagination = function() {
    
}
navigationNS.Pagination.prototype = function() {
    var getPage = function() {
        var a = $(this);

        var options = {
            url: a.attr('href'),
            data: $('#form0').serialize(),
            type: 'get'
        }

        $.ajax(options).done(function(data) {
            var target = a.parents('div.pagedList').attr('data-pl-target');
            $(target).replaceWith(data);
        });

        return false;
    }
    return {getPage : getPage}
}();
