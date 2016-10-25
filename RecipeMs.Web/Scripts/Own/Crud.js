var crudNs = crudNs || {};

crudNs.Tab = function (current, tab) {
    this.tabTarget = tab;
    this.tabTargetElement = tab ?  $(tab) : undefined;
    this.href = current.attr('href');
    this.curr = $(current);
    this.divTarget = this.curr.data('crud-target');
    this.itemName = this.curr.data('crud-name');
    this.divTable = this.curr.data('crud-table');
    this.deleteAction = this.curr.data('crud-action');
    this.entityId = this.curr.data('crud-id');
    this.IdName = this.curr.data('crud-idname');
}
crudNs.Tab.prototype = function () {
    var activateSelectedTab = function () {
        var self = this;
        var tab = self.tabTargetElement;

        var linkTabs = tab.closest('.nav-tabs-custom').find('.nav-tabs li');
        var panes = tab.closest('div .tab-content').find('.tab-pane');
        var selectedTab = linkTabs.find('[href ="' + self.tabTarget + '"]').parent();

        linkTabs.removeClass('active');
        panes.removeClass('active');

        selectedTab.addClass('active');
        tab.closest('.tab-pane').addClass('active');
    },
    populateDll = function() {
        var self = this;

        var options = {
            url: self.href,
            type: 'get'
        }

        $.ajax(options).done(function (data) {
            $(self.divTarget).replaceWith(data);
        });
    },
    populateTable = function () {
        var self = this;

        var options = {
            url: self.href,
            type: 'get'
        }

        $.ajax(options).done(function (data) {
            $(self.divTable).replaceWith(data);
        });
    },
    removeChild = function(parentId, childId) {
        var self = this;

        self.href = this.deleteAction;

        var options = {
            url: self.href,
            type: 'post',
            contentType: 'application/json; charset=utf-8',
            datatype: 'json',
            data: JSON.stringify({ parentId: parentId, childId: childId })
        }
        $.ajax(options).done(reloadTable);
    },
    init = function () {
        var self = this;

        $(self.tabTarget + ' .box-header h3').text(self.itemName);
        $(self.divTarget + ' '+ self.IdName).val(self.entityId);

        populateTable.call(self);
        activateSelectedTab.call(self);

        return false;
    };
    return { init: init, populateDll : populateDll, populateTable : populateTable, removeChild:removeChild };
}();