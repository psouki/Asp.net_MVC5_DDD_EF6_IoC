var searchNS = searchNS || {};

searchNS.Autocomplete = function () {
    this.targetElement = 'data-target';
    this.container = '#containerAc';
    this.sourceAttibute = 'data-ac-autocomplete';
}
searchNS.Autocomplete.prototype = function () {
    var ajaxSubmit = function (form) {
        var self = this;

        var options = {
            url: form.attr('action'),
            type: form.attr('method'),
            data: form.serialize()
        };

        $.ajax(options).done(function (data) {
            var target = $(form.attr(self.targetElement));
            target.replaceWith(data);
        });

        return false;
    },
    submitAutocompleteForm = function(event, ui) {
        var input = $(this);
        input.val(ui.item.label);

        var form = input.parents('form:first');
        form.submit();
    },
    createAutocomplete = function (input) {
        var self = this;

        var options = {
            minLength:2,
            source: input.attr(self.sourceAttibute),
            select: submitAutocompleteForm,
            appendTo: self.container
        };

        input.autocomplete(options);
    };
    return { ajaxSubmit: ajaxSubmit, createAutocomplete: createAutocomplete }
}();


