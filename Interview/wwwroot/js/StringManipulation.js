$(function () {

    function StringManipulationVm() {
        var self = this;
        self.stringValue = ko.observable();
        self.resultValue = ko.observable();
        self.invalidText = "Please enter some text into the text box.";
        self.selectedFunction = ko.observable();
        self.isLoading = ko.observable(false);

        //we can do the functions client side or server side
        self.performServerSide = ko.observable(true);
        
        //prevents the error message displaying on page load
        self.showErrors = ko.observable(false);

        //should we display the result section?
        self.resultsVisible = ko.observable(false);

        self.functionTitle = ko.computed(function() {
            var selectedFunction = self.selectedFunction();
            var text = "";
            if (selectedFunction === 0)
                text = "All letters in the phrase turn into uppercase";
            if (selectedFunction === 1)
                text = "All letters in the phrase are reversed";

            return text;
        });

        //can we submit the data?
        self.isValid = ko.computed(function() {
            var value = self.stringValue();
            if (value === undefined || value === null || value === "") {
                return false;
            }
            return true;
        });

        //should we show the error message for the input box?
        self.showError = ko.computed(function() {
            if (self.showErrors() && !self.isValid()) {
                self.resultsVisible(false);
                return true;
            }

            if (self.stringValue() !== undefined && self.stringValue().length === 0) {
                self.resultsVisible(false);
                return true;
            } else
                return false;
        });

        //function called when option button is clicked
        self.optionClick = function(option) {
            if (self.isLoading()) {
                alert("Please wait for existing action to complete. Thanks.");
                return;
            }

            self.showErrors(true);
            if (self.isValid()) {
                self.resultsVisible(true);
                self.selectedFunction(option);
                self.getResult();
            }

            //scroll page to the top of the result section div
            $("html, body").animate({scrollTop: $("section#stringmanipulation").offset().top - $("nav").height()},1000);
        };

        self.getResult = function() {
            self.isLoading(true);
            if (self.performServerSide()) {
                //ajax call to controller and return result  
                $.ajax({
                    url: $("#getresulturl").val(),
                    type: "POST",
                    dataType: "json",
                    data: {
                        phrase: self.stringValue(),
                        task: self.selectedFunction()
                    },
                    success: function(response) {
                        if (response.success) {
                            self.resultValue(response.result);
                        }
                    }
                }).done(function() {
                    self.isLoading(false);
                });
            } else {
                // we can use javascript functions to do it instead                        
                self.resultValue(self.clientSideFunctions(self.selectedFunction()));
                self.isLoading(false);
            }
        };

        self.clientSideFunctions = function(selectedFunction) {
            switch (selectedFunction) {
            case 0://uppercase
                return self.uppercase();
            case 1://reverse
                return self.reverse();
            default:
                return null;
            }
        };

        self.uppercase = function() {
            var text = self.stringValue();
            return text.toUpperCase();
        };

        self.reverse = function() {
            var text = self.stringValue();
            return text.split("").reverse().join("");
        };
    }

    var viewModel = new StringManipulationVm();
    ko.applyBindings(viewModel);
});