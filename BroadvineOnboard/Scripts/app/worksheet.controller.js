(function () {
    'use strict';

    angular
        .module("ob")
        .controller("WorksheetController", WorksheetController)

    function WorksheetController() {
        var vm = this;
        var defaultValue = 1;

        vm.value = defaultValue;
        vm.options = {
            min: "1",
            max: "100",
            format: "n0"
        };

        vm.onBlur = function () {
            if (vm.value === null) {
                vm.value = defaultValue;
            }
        };
    }

})();