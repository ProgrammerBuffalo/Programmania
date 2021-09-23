var performanceChart;
var curDate = new Date();
var lastDate = new Date();
var offset = 0;

var monthNames;
var dayNames;

function getDaysInMonth() {
    return new Date(curDate.getFullYear(), curDate.getMonth(), 0).getDate();
}

function getDayLabels(days) {
    let labels = new Array(days);
    for (var i = 0; i < days; i++)
        labels[i] = i + 1;
    return labels;
}

function getWeekLabels() {
    let date = new Date(curDate);
    let labels = new Array(7);
    let month;
    for (var i = 0; i < 7; i++) {
        month = monthNames[date.getMonth()];
        labels[i] = date.getDate() + '-' + month[0] + month[1] + month[2];
        date.setDate(date.getDate() + 1);
    }
    return labels;
}

function getChartDayData(rewards) {
    if (rewards != null) {
        let chartData = new Array(getDaysInMonth());
        let tempDate1, tempDate2;
        let exp = 0;
        let k = 0;
        tempDate1 = new Date(rewards[0].date);
        for (var i = 0; i < rewards.length - 1; i++) {
            exp += rewards[i].expierence;
            tempDate2 = new Date(rewards[i + 1].date);
            if (tempDate1.getDate() != tempDate2.getDate()) {
                chartData[k] = { day: tempDate1.getDate(), exp: exp };
                tempDate1 = tempDate2;
                k++;
                exp = 0;
            }
        }
        exp += rewards[i].expierence;
        chartData[k] = { day: tempDate1.getDate(), exp: exp };
        return chartData;
    }
    return null;
}

function getChartWeekData(rewards, labels) {
    if (rewards != null) {
        let date = new Date(curDate);
        let chartData = new Array(7);
        let dates = new Array(7);

        let k = 0;
        let exp = 0;
        let tempDate1, tempDate2;
        tempDate1 = new Date(rewards[0].date);

        for (var i = 0; i < dates.length; i++) {
            dates[i] = new Date(date);
            date.setDate(date.getDate() + 1);
        }

        for (var i = 0; i < rewards.length - 1; i++) {
            exp += rewards[i].expierence;
            tempDate2 = new Date(rewards[i + 1].date);
            if (tempDate1.getDate() != tempDate2.getDate() || tempDate1.getMonth() != tempDate2.getMonth()) {
                for (var j = 0; j < dates.length; j++) {
                    if (dates[j].getMonth() == tempDate1.getMonth() && dates[j].getDate() == tempDate1.getDate()) {
                        chartData[k] = { week: labels[j], exp: exp };
                        tempDate1 = tempDate2;
                        k++;
                        exp = 0;
                        break;
                    }
                }
            }
        }
        exp += rewards[i].expierence;
        for (var j = 0; j < dates.length; j++) {
            if (dates[j].getMonth() == tempDate1.getMonth() && dates[j].getDate() == tempDate1.getDate()) {
                chartData[k] = { week: labels[j], exp: exp };
                break;
            }
        }
        return chartData;
    }
    return null;
}

function getChartMonthData(rewards, labels) {
    if (rewards != null) {
        let chartData = new Array(12);
        let exp = 0;
        let k = 0;
        let tempDate1, tempDate2;
        tempDate1 = new Date(rewards[0].date);
        for (var i = 0; i < rewards.length - 1; i++) {
            exp += rewards[i].expierence;
            tempDate2 = new Date(rewards[i + 1].date);
            if (tempDate1.getMonth() != tempDate2.getMonth()) {
                chartData[k] = { month: labels[tempDate1.getMonth()], exp: exp };
                tempDate1 = tempDate2;
                k++;
                exp = 0;
            }
        }
        exp += rewards[k].expierence;
        chartData[k] = { month: labels[tempDate1.getMonth()], exp: exp };
        return chartData;
    }
    return null;
}

function getWeekHeader() {
    let date = new Date(curDate);
    date.setDate(date.getDate() + 6);
    if (curDate.getMonth() == date.getMonth())
        return curDate.getDate() + '/' + date.getDate() + ' ' + monthNames[curDate.getMonth()];
    else
        return curDate.getDate() + '/' + date.getDate() + ' ' + monthNames[curDate.getMonth()] + '/' + monthNames[date.getMonth()]
}

function updateChart(rewards, type) {
    if (type == 'Month') {
        performanceChart = createChart(getChartMonthData(rewards, monthNames), monthNames, curDate.getFullYear(), 'month');
    }
    else if (type == 'Week') {
        let labels = getWeekLabels();
        performanceChart = createChart(getChartWeekData(rewards, labels), labels, getWeekHeader(), 'week');
    }
    else if (type == 'Day') {
        let days = getDaysInMonth();
        let labels = getDayLabels(days);
        performanceChart = createChart(getChartDayData(rewards), labels, monthNames[curDate.getMonth()] + ' - ' + curDate.getFullYear(), 'day');
    }
}

function createChart(data, labels, headerLabel, xAxis) {
    var ctx = document.getElementById('performanceChart').getContext('2d');
    performanceChart = new Chart(ctx, {
        type: 'line',
        data: {
            labels: labels,
            datasets: [{
                label: headerLabel,
                data: data,
                fill: true,
                borderColor: 'rgb(75, 192, 192)',
                tension: 0.1,
            }]
        },
        options: {
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
            parsing: {
                xAxisKey: xAxis,
                yAxisKey: 'exp'
            },
            responsive: true,
            maintainAspectRatio: false
        },

    });
    return performanceChart;
}

$(document).ready(function () {
    let type = $('#chart-type').val();

    $.ajax({
        type: 'GET',
        url: 'performance/rewards-init',
        dataType: 'json',
        processData: true,
        data: { 'type': type, 'date': curDate.toISOString() },
        success: function (data) {
            monthNames = data.months;
            dayNames = data.days;

            updateChart(data.rewards, type);
        }
    })
});


$(".chart-arrow-left").click(function () {
    let type = $('#chart-type').val();
    performanceChart.destroy();

    $.ajax({
        type: 'GET',
        url: 'performance/rewards',
        dataType: 'json',
        processData: true,
        data: JSON.stringify({ 'type': type, 'date': curDate.toISOString() }),
        contentType: 'application/json',
        success: function (data) {
            if (type == "Month")
                curDate.setFullYear(curDate.getFullYear() - 1);
            else if (type == "Week")
                curDate.setDate(curDate.getDate() - 7);
            else if (type == "Day")
                curDate.setMonth(curDate.getMonth() - 1);

            updateChart(data, type);
        }
    });
});

$(".chart-arrow-right").click(function () {
    let type = $('#chart-type').val();
    performanceChart.destroy();

    $.ajax({
        type: 'GET',
        url: 'performance/rewards',
        dataType: 'json',
        processData: true,
        data: { 'type': type, 'date': curDate.toISOString() },
        success: function (data) {
            if (type == "Month")
                curDate.setFullYear(curDate.getFullYear() + 1);
            else if (type == "Week")
                curDate.setDate(curDate.getDate() + 7);
            else if (type == "Day")
                curDate.setMonth(curDate.getMonth() + 1);

            updateChart(data, type);
        }
    });
});

$('#chart-type').change(function () {
    let type = $('#chart-type').val();
    performanceChart.destroy();

    $.ajax({
        type: 'GET',
        url: 'performance/rewards',
        dataType: 'json',
        processData: true,
        data: { 'type': type, 'date': curDate.toISOString() },
        success: function (data) {
            updateChart(data, type);
        }
    });
});

$("#chart-more").click(function () {
    $.ajax({
        type: 'GET',
        url: 'performance/rewards-more',
        data: { 'count': $('#load-count').val(), 'offset': offset },
        processData: false,
        contentType: false,
        success: function (data) {
            if (data != null) {
                offset += data.length;
                var body = $('.table tbody').filter(':last');
                for (var i = 0; i < data.length; i++) {
                    let date = new Date(data[i].date);
                    if (date.getFullYear() != lastDate.getFullYear()
                        || date.getMonth() != lastDate.getMonth()
                        || date.getDate() != lastDate.getDate()) {

                        $('#history_day').append(`<h3 class='day-title'>${date.getDate()} ${monthNames[date.getMonth()]}</h3>`);
                        let div = document.createElement('div');
                        $(div).addClass('day-body table-responsive');
                        let table = document.createElement('table');
                        $(table).addClass('table table-hover table-bordered table-striped table-dark');
                        $(table).append(createRewardsHeader());

                        div.append(table);
                        $('#history_day').append(div);
                        body = $(table).find('tbody');
                    }
                    $(body).append(addTableRewards(data[i], date));
                    lastDate = date;
                }
            }
        }
    });
});