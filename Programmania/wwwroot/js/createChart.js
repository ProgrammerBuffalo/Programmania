var dataArray = [25, 15, 30];
const labels = ["January", "February", "March"];
const data = {
    labels: labels,
    datasets: [{
        label: 'Perfomance',
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
        animations: {
            tension: {
                duration: 1000,
                easing: 'linear',
                from: 1,
                to: 0,
                loop: true
            }
        },
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
        responsive: true
    },
    
});