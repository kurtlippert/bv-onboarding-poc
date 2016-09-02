(function () {
    'use strict';

    angular
        .module('ob')
        .controller('PopupController', PopupController)

    //PopupController.$inject = ['']

    function PopupController() {
        var vm = this;
        vm.file = {};

        vm.fileChanged = function (element) {
            vm.file = element.files[0];
        }

        vm.options = {
            title: "title",
            modal: true,
            width: "700",
            height: "500",
            resizable: false,
            visible: false
        };

        vm.show = function () {
            var data = new FormData();
            data.append("file", vm.file);

            var opts = {
                url: Ob.Urls.WizardProperty,
                data: data,
                cache: false,
                contentType: false,
                processData: false,
                type: 'POST',
                success: function (callback) {
                    if (!callback.startsWith("<!DOCTYPE")) {
                        vm.popupWizard.content(callback);
                        vm.popupWizard.center();
                        vm.popupWizard.open();
                    }
                    else {
                        if (!window.location.href.endsWith("?msg=The%20file%20contained%20no%20data.")) {
                            window.location.href = window.location.href + "?msg=The%20file%20contained%20no%20data.";
                        }
                    }
                }
            };

            if (data.fake) {
                // Make sure no text encoding stuff is done by xhr
                opts.xhr = function () { var xhr = $.ajaxSettings.xhr(); xhr.send = xhr.sendAsBinary; return xhr; }
                opts.contentType = "multipart/form-data; boundary=" + data.boundary;
                opts.data = data.toString();
            }

            vm.popupWizard.refresh(opts);
            //vm.popupWizard.center();
            //vm.popupWizard.open();
        };
    }

})();