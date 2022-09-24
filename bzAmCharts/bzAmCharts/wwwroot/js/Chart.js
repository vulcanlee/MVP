
function CreateColumnChart(chartId,chartData) {
    /**
 * ---------------------------------------
 * This demo was created using amCharts 4.
 *
 * For more information visit:
 * https://www.amcharts.com/
 *
 * Documentation is available at:
 * https://www.amcharts.com/docs/v4/
 * ---------------------------------------
 */

    // Apply chart themes
    am4core.useTheme(am4themes_animated);
    am4core.useTheme(am4themes_kelly);

    // Create chart instance
    var chart = am4core.create(chartId, am4charts.XYChart);

    chart.marginRight = 400;

    // Add data
    //chart.data = [{
    //    "country": "Lithuania",
    //    "research": 501.9,
    //    "marketing": 250,
    //    "sales": 199
    //}, {
    //    "country": "Czech Republic",
    //    "research": 301.9,
    //    "marketing": 222,
    //    "sales": 251
    //}, {
    //    "country": "Ireland",
    //    "research": 201.1,
    //    "marketing": 170,
    //    "sales": 199
    //}, {
    //    "country": "Germany",
    //    "research": 165.8,
    //    "marketing": 122,
    //    "sales": 90
    //}, {
    //    "country": "Australia",
    //    "research": 139.9,
    //    "marketing": 99,
    //    "sales": 252
    //}, {
    //    "country": "Austria",
    //    "research": 128.3,
    //    "marketing": 85,
    //    "sales": 84
    //}, {
    //    "country": "UK",
    //    "research": 99,
    //    "marketing": 93,
    //    "sales": 142
    //}, {
    //    "country": "Belgium",
    //    "research": 60,
    //    "marketing": 50,
    //    "sales": 55
    //}, {
    //    "country": "The Netherlands",
    //    "research": 50,
    //    "marketing": 42,
    //    "sales": 25
    //    }];
    //chart.data = JSON.stringify(chartData); ;
    chart.data = chartData; ;

    //console.log('chart', chart);

    // Create axes
    var categoryAxis = chart.xAxes.push(new am4charts.CategoryAxis());
    categoryAxis.dataFields.category = "country";
    categoryAxis.title.text = "Local country offices";
    categoryAxis.renderer.grid.template.location = 0;
    categoryAxis.renderer.minGridDistance = 20;


    var valueAxis = chart.yAxes.push(new am4charts.ValueAxis());
    valueAxis.title.text = "Expenditure (M)";

    // Create series
    var series = chart.series.push(new am4charts.ColumnSeries());
    series.dataFields.valueY = "research";
    series.dataFields.categoryX = "country";
    series.name = "Research";
    series.tooltipText = "{name}: [bold]{valueY}[/]";
    series.stacked = true;

    var series2 = chart.series.push(new am4charts.ColumnSeries());
    series2.dataFields.valueY = "marketing";
    series2.dataFields.categoryX = "country";
    series2.name = "Marketing";
    series2.tooltipText = "{name}: [bold]{valueY}[/]";
    series2.stacked = true;

    var series3 = chart.series.push(new am4charts.ColumnSeries());
    series3.dataFields.valueY = "sales";
    series3.dataFields.categoryX = "country";
    series3.name = "Sales";
    series3.tooltipText = "{name}: [bold]{valueY}[/]";
    series3.stacked = true;

    // Add cursor
    chart.cursor = new am4charts.XYCursor();
}

function CreatePieChart(chartId) {
    /**
     * ---------------------------------------
     * This demo was created using amCharts 4.
     *
     * For more information visit:
     * https://www.amcharts.com/
     *
     * Documentation is available at:
     * https://www.amcharts.com/docs/v4/
     * ---------------------------------------
     */

    // Create chart instance
    var chart = am4core.create(chartId, am4charts.PieChart);

    // Add data
    chart.data = [{
        "country": "Lithuania",
        "litres": 501.9
    }, {
        "country": "Czechia",
        "litres": 301.9
    }, {
        "country": "Ireland",
        "litres": 201.1
    }, {
        "country": "Germany",
        "litres": 165.8
    }, {
        "country": "Australia",
        "litres": 139.9
    }, {
        "country": "Austria",
        "litres": 128.3
    }, {
        "country": "UK",
        "litres": 99
    }, {
        "country": "Belgium",
        "litres": 60
    }, {
        "country": "The Netherlands",
        "litres": 50
    }];

    // Add and configure Series
    var pieSeries = chart.series.push(new am4charts.PieSeries());
    pieSeries.dataFields.value = "litres";
    pieSeries.dataFields.category = "country";
}

function CreateLineChart(chartId) {
    /**
     * ---------------------------------------
     * This demo was created using amCharts 4.
     *
     * For more information visit:
     * https://www.amcharts.com/
     *
     * Documentation is available at:
     * https://www.amcharts.com/docs/v4/
     * ---------------------------------------
     */

    am4core.useTheme(am4themes_animated);

    // Create chart instance
    var chart = am4core.create(chartId, am4charts.XYChart);

    // Add data
    chart.data = [{
        "date": new Date(2018, 0, 1),
        "value": 450,
        "value2": 162,
        "value3": 1100
    }, {
        "date": new Date(2018, 0, 2),
        "value": 669,
        "value3": 841
    }, {
        "date": new Date(2018, 0, 3),
        "value": 1200,
        "value3": 199
    }, {
        "date": new Date(2018, 0, 4),
        "value2": 867
    }, {
        "date": new Date(2018, 0, 5),
        "value2": 185,
        "value3": 669
    }, {
        "date": new Date(2018, 0, 6),
        "value": 150
    }, {
        "date": new Date(2018, 0, 7),
        "value": 1220,
        "value2": 350,
        "value3": 600
    }];

    // Create axes
    var dateAxis = chart.xAxes.push(new am4charts.DateAxis());
    dateAxis.renderer.grid.template.location = 0;
    dateAxis.renderer.minGridDistance = 30;

    var valueAxis = chart.yAxes.push(new am4charts.ValueAxis());

    // Create series
    function createSeries(field, name) {
        var series = chart.series.push(new am4charts.LineSeries());
        series.dataFields.valueY = field;
        series.dataFields.dateX = "date";
        series.name = name;
        series.tooltipText = "{dateX}: [b]{valueY}[/]";
        series.strokeWidth = 2;

        series.smoothing = "monotoneX";

        var bullet = series.bullets.push(new am4charts.CircleBullet());
        bullet.circle.stroke = am4core.color("#fff");
        bullet.circle.strokeWidth = 2;

        return series;
    }

    createSeries("value", "Series #1");
    createSeries("value2", "Series #2");
    createSeries("value3", "Series #3");

    chart.legend = new am4charts.Legend();
    chart.cursor = new am4charts.XYCursor();
}