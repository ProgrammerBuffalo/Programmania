var labels = [];
var dataArray = [25, 15, 30, 12, 45, 12, 57, 47, 1, 3, 67, 34, 25, 15, 30, 12, 45, 12, 57, 47, 1, 3, 67, 34, 45, 97, 57, 58, 36, 48];
for (let i = 0; i < 30; i++) {
    labels[i] = i + 1;
}

const data = {
    labels: labels,
    datasets: [{
        label: "Rewards",
        data: dataArray,
        fill: false,
        borderColor: "rgb(97,1,6)",
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
        maintainAspectRatio: false
    },
})