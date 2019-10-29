/// <reference path="jquery-1.5.1-vsdoc.js" />
/// <reference path="jquery-ui.js" />
/// <reference path="jquery-3.3.1.min.js" />
/// <reference path="jquery-ui-1.8.11.js" />
$(document).ready(function () {
    $('*[data-autocomplete-url]')
        .each(function () {
            $(this).autocomplete({
                source: $(this).data("autocomplete-url")
            });
        });
});