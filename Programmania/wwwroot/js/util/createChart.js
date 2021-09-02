const monthNames = ["January", "February", "March", "April", "May", "June",
    "July", "August", "September", "October", "November", "December"
];

function getDaysCount(month, year) {
    return new Date(year, month, 0).getDate();
}

function setCurrentMonth(monthCount) {
    return monthNames[monthCount];
}

function setDays(monthCount, year) {
    return getDaysCount(monthCount + 1,year);
}

function setLabels(days) {
    for (let i = 0; i < days; i++) {
        labels.push(i + 1);
    }
    return labels;
}

var date = new Date();
var monthCount = date.getMonth();
var currMonth = setCurrentMonth(monthCount);
var year = date.getFullYear();
var dataArray = [25, 15, 30, 12, 45, 12, 57, 47, 1, 3, 67, 34, 25, 15, 30, 12, 45, 12, 57, 47, 1, 3, 67, 34, 45, 97, 57, 58, 36, 48];
var labels = [];
var days = setDays(monthCount, year);


labels = setLabels(days);


var perfomanceChart = CreateChart(labels, currMonth);
function CreateChart(labels, month) {
    const data = {
        labels: labels,
        datasets: [{
            label: month,
            data: dataArray,
            fill: false,
            borderColor: 'rgb(75, 192, 192)',
            tension: 0.1
        }]
    };

    var ctx = document.getElementById('perfomanceChart').getContext('2d');
    var perfomanceChart = new Chart(ctx, {
        type: 'line',
        data: data,
        options: {

            scales: {
                y: {
                    min: 0,
                    max: 100
                }
            },
            layout: {
                padding: 20
            },
            plugins: {
                legend: {
                    labels: {
                        font: {
                            size: 20
                        }
                    }
                }
            },
            responsive: true,
            maintainAspectRadio: false
        },

    });
    return perfomanceChart;
}

$(".chart-arrow-left").click(function () {
    if (monthCount == 0) {
        monthCount = 11;
        year--;
    }
    else {
        monthCount--;
    }   
    currMonth = setCurrentMonth(monthCount);
    days = setDays(monthCount, year);
    labels = [];
    labels = setLabels(days);
    console.log(labels);
    perfomanceChart.destroy();
    perfomanceChart = CreateChart(labels, currMonth);
});

$(".chart-arrow-right").click(function () {
    if (monthCount == 11) {
        monthCount = 0;
        year++;
    }
    else {
        monthCount++;
    }
    currMonth = setCurrentMonth(monthCount);
    days = setDays(monthCount, year);
    labels = [];
    labels = setLabels(days);
    console.log(labels);
    perfomanceChart.destroy();
    perfomanceChart = CreateChart(labels, currMonth);
});

var mediaQuery = window.matchMedia("(max-width: 445px)");

function mediaMatch(mediaQuery) {
    if (mediaQuery.matches) {
        perfomanceChart.options.responsive = false;
        perfomanceChart.update();
    }
    else {
        perfomanceChart.options.responsive = true;
        perfomanceChart.update();
    }
}

mediaMatch(mediaQuery);
mediaQuery.addListener(mediaMatch);