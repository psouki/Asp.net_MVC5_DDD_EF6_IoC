var nutriLabelNS = nutriLabelNS || {};

nutriLabelNS.Build = function (nutritionalFacts) {
    this.facts = nutritionalFacts;
};

nutriLabelNS.Build.prototype = function () {
    var buildFootnotes = function(facts, divMaster) {
        var divRowfootnote = document.createElement('div');
        divRowfootnote.className = 'row';

        var divFootnote = document.createElement('div');
        divFootnote.className = 'footnote';

        var spanStar = document.createElement('span');
        spanStar.className = 'star';
        spanStar.innerHTML = '*';

        divFootnote.appendChild(spanStar);
        divFootnote.innerHTML += ' Percent Daily Values are based on a 2000 calorie diet. Your daily values may be higher or lower depending on your calorie needs.';

        divRowfootnote.appendChild(divFootnote);
        divMaster.append(divRowfootnote);
       },
        buildMain = function (nutrients, divMaster) {
            var divDaily = document.createElement('div');
            divDaily.className = 'line text-bold';
            divDaily.innerHTML = '% Daily Value';
            var sup = document.createElement('sup');
            sup.innerHTML = '*';
            divDaily.appendChild(sup);

            var divMain = document.createElement('div');
            var divAuxiliary = document.createElement('div');

            for (var i = 0, nutrient; i < nutrients.length; i++) {
                nutrient = nutrients[i];
                var divLineMain = document.createElement('div');
                divLineMain.className = 'line';

                var divRowMain = document.createElement('div');
                divRowMain.className = 'row';

                var divNutrientName = document.createElement('div');
                divNutrientName.innerHTML = nutrient.Name + ' ' + nutrient.Value + nutrient.Unit;
                if (nutrient.Type === 1) {
                    divNutrientName.className = nutrient.IsTitle ? 'col-sm-8 text-bold' : 'col-sm-8 indent';
                } else if (nutrient.Type === 2) {
                    divNutrientName.className = 'col-sm-8';
                }

                var divDailyValue = document.createElement('div');
                divDailyValue.className = 'pull-right col-sm-3';
                divDailyValue.innerHTML = nutrient.ShowPercentage ? nutrient.Percentage + '%' : ' ';

                divRowMain.appendChild(divNutrientName);
                divRowMain.appendChild(divDailyValue);
                divLineMain.appendChild(divRowMain);

                if (nutrient.Type === 1) {
                    divMain.appendChild(divLineMain);
                } else if (nutrient.Type === 2) {
                    divAuxiliary.appendChild(divLineMain);
                }
            }

            var divBar2Main = document.createElement('div');
            divBar2Main.className = 'bar2';

            divMaster.append(divMain);
            divMaster.append(divBar2Main);
            divMaster.append(divAuxiliary);

            return divMaster;
        },
        buildHead = function (facts, divMaster) {
            var divTitle = document.createElement('div');
            divTitle.className = 'title';
            divTitle.innerHTML = ' Nutrition Facts';

            var divBar1Head = document.createElement('div');
            divBar1Head.className = 'bar1';

            var divAmount = document.createElement('div');
            divAmount.className = 'line text-bold';
            divAmount.innerHTML = 'Serving Size: ' + facts.Amount + ' g';

            var divCalorie = document.createElement('div');
            divCalorie.className = 'line';

            var divRowHead = document.createElement('div');
            divRowHead.className = 'row';

            var divCalName = document.createElement('div');
            divCalName.className = 'col-sm-6';
            divCalName.innerHTML = 'Calories ' + facts.Calories;

            var divCalValue = document.createElement('div');
            divCalValue.className = 'pull-right col-sm-6';
            divCalValue.innerHTML = 'Calories from Fat ' + facts.CaloriesFromFat.toFixed(0);
            
            var divBar2Head = document.createElement('div');
            divBar2Head.className = 'bar2';

            divRowHead.appendChild(divCalName);
            divRowHead.appendChild(divCalValue);
            divCalorie.appendChild(divRowHead);

            divMaster.append(divTitle);
            divMaster.append(divBar1Head);
            divMaster.append(divAmount);
            divMaster.append(divCalorie);
            divMaster.append(divBar2Head);

            return divMaster;
        },
        init = function() {
            var self = this;
            var divMaster = $('#NutritionalLabel');
            divMaster.html('');
            divMaster = buildHead(self.facts, divMaster);
            divMaster = buildMain(self.facts.Nutrients, divMaster);
            divMaster = buildFootnotes(self.facts, divMaster);
        };
    return { init: init };
}();

nutriLabelNS.Summary = function(nutritionalFacts) {
    this.facts = nutritionalFacts;
};

nutriLabelNS.Summary.prototype = function() {
    var populateValues = function(facts) {
            var chartValues = [
                {
                    percentage: facts.FatPercentage,
                    color :'#cc0000' 
                },
                {
                    percentage: facts.CarbPercentage,
                    color :'#006400' 
                },
                {
                    percentage: facts.ProteinPercentage,
                    color :'#0066cc' 
                }
            ];
            return chartValues;
        },
    buildPercentages = function (facts) {
        var divFatPercentage = document.createElement('div');
        divFatPercentage.className = 'col-sm-3 fact fat';
        divFatPercentage.innerHTML = facts.FatPercentage+'%';

        var divCarbPercentage = document.createElement('div');
        divCarbPercentage.className = 'col-sm-3 fact carb';
        divCarbPercentage.innerHTML = facts.CarbPercentage+'%';

        var divProteinPercentage = document.createElement('div');
        divProteinPercentage.className = 'col-sm-3 fact protein';
        divProteinPercentage.innerHTML = facts.ProteinPercentage+'%';

        var summaryValues = $('#summaryPercentages');
        summaryValues.html(' ');
        summaryValues.append(divFatPercentage);
        summaryValues.append(divCarbPercentage);
        summaryValues.append(divProteinPercentage);
    },
    buildCanvas = function(facts) {
        var canvas = document.getElementById('calorieDistribution');
        var context = canvas.getContext('2d');

        var initialPosition = 15;
        var chartWidth = 265;
        var onePercentValue = (chartWidth - initialPosition) / 100;

        var start = initialPosition;
        var finish;

        var values = populateValues(facts);
        for (var i = 0, value; i < values.length; i++) {
            value = values[i];

            finish = start + (value.percentage * onePercentValue);

            context.beginPath();
            context.moveTo(start, 15);
            context.lineTo(finish, 15);
            context.lineWidth = 10;
            context.strokeStyle = value.color;
            context.stroke();

            start = finish;
        }
    },
    buildValues = function(facts) {
        var divFatValue = document.createElement('div');
        divFatValue.className = 'col-sm-3 fact fat';
        divFatValue.innerHTML = facts.CaloriesFromFat.toFixed(1);

        var divCarbValue = document.createElement('div');
        divCarbValue.className = 'col-sm-3 fact carb';
        divCarbValue.innerHTML = facts.CaloriesFromCarb.toFixed(1);

        var divProteinValue = document.createElement('div');
        divProteinValue.className = 'col-sm-3 fact protein';
        divProteinValue.innerHTML = facts.CaloriesFromProtein.toFixed(1);

        var summaryValues = $('#summaryValues');
        summaryValues.html(' ');
        summaryValues.append(divFatValue);
        summaryValues.append(divCarbValue);
        summaryValues.append(divProteinValue);
    },
    buildLegend = function (facts) {
        $('#summaryCalorie').html(facts.Calories + ' Kcal');
    },
    init = function() {
        var self = this;
        buildLegend(self.facts);
        buildValues(self.facts);
        buildCanvas(self.facts);
        buildPercentages(self.facts);
    };
    return { init: init };
}();





