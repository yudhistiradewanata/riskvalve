const assessmentIntegrityveryhigh = parseInt(
  getInputVal("#assessmentIntegrityveryhigh")
);
const assessmentIntegrityhigh = parseInt(
  getInputVal("#assessmentIntegrityhigh")
);
const assessmentIntegritymedium = parseInt(
  getInputVal("#assessmentIntegritymedium")
);
const assessmentIntegritylow = parseInt(getInputVal("#assessmentIntegritylow"));
const assessmentIntegrityverylow = parseInt(
  getInputVal("#assessmentIntegrityverylow")
);
const assessmentMap1A = parseInt(getInputVal("#assessmentMap1A"));
const assessmentMap1B = parseInt(getInputVal("#assessmentMap1B"));
const assessmentMap1C = parseInt(getInputVal("#assessmentMap1C"));
const assessmentMap1D = parseInt(getInputVal("#assessmentMap1D"));
const assessmentMap1E = parseInt(getInputVal("#assessmentMap1E"));
const assessmentMap2A = parseInt(getInputVal("#assessmentMap2A"));
const assessmentMap2B = parseInt(getInputVal("#assessmentMap2B"));
const assessmentMap2C = parseInt(getInputVal("#assessmentMap2C"));
const assessmentMap2D = parseInt(getInputVal("#assessmentMap2D"));
const assessmentMap2E = parseInt(getInputVal("#assessmentMap2E"));
const assessmentMap3A = parseInt(getInputVal("#assessmentMap3A"));
const assessmentMap3B = parseInt(getInputVal("#assessmentMap3B"));
const assessmentMap3C = parseInt(getInputVal("#assessmentMap3C"));
const assessmentMap3D = parseInt(getInputVal("#assessmentMap3D"));
const assessmentMap3E = parseInt(getInputVal("#assessmentMap3E"));
const assessmentMap4A = parseInt(getInputVal("#assessmentMap4A"));
const assessmentMap4B = parseInt(getInputVal("#assessmentMap4B"));
const assessmentMap4C = parseInt(getInputVal("#assessmentMap4C"));
const assessmentMap4D = parseInt(getInputVal("#assessmentMap4D"));
const assessmentMap4E = parseInt(getInputVal("#assessmentMap4E"));
const assessmentMap5A = parseInt(getInputVal("#assessmentMap5A"));
const assessmentMap5B = parseInt(getInputVal("#assessmentMap5B"));
const assessmentMap5C = parseInt(getInputVal("#assessmentMap5C"));
const assessmentMap5D = parseInt(getInputVal("#assessmentMap5D"));
const assessmentMap5E = parseInt(getInputVal("#assessmentMap5E"));
const jsonAssessmentPieChartVar = getInputVal("#jsonAssessmentPieChart");
const jsonAssessmentBarChartVar = getInputVal("#jsonAssessmentBarChart");

$("#risk-barchart-horizontal").highcharts({
  chart: {
    type: "bar",
  },
  title: {
    text: "",
  },
  xAxis: {
    categories: [""],
  },
  yAxis: {
    title: {
      text: "",
    },
    tickInterval: 1,
  },
  series: [
    {
      name: "Very High Priority",
      data: [assessmentIntegrityveryhigh],
    },
    {
      name: "High Priority",
      data: [assessmentIntegrityhigh],
    },
    {
      name: "Medium Priority",
      data: [assessmentIntegritymedium],
    },
    {
      name: "Low Priority",
      data: [assessmentIntegritylow],
    },
    {
      name: "Very Low Priority",
      data: [assessmentIntegrityverylow],
    },
  ],
  colors: ["red", "orange", "yellow", "green", "#81B014"],
});
const rawHeatmapData = [
  { x: 0, y: 0, value: assessmentMap1A },
  { x: 0, y: 1, value: assessmentMap1B },
  { x: 0, y: 2, value: assessmentMap1C },
  { x: 0, y: 3, value: assessmentMap1D },
  { x: 0, y: 4, value: assessmentMap1E },
  { x: 1, y: 0, value: assessmentMap2A },
  { x: 1, y: 1, value: assessmentMap2B },
  { x: 1, y: 2, value: assessmentMap2C },
  { x: 1, y: 3, value: assessmentMap2D },
  { x: 1, y: 4, value: assessmentMap2E },
  { x: 2, y: 0, value: assessmentMap3A },
  { x: 2, y: 1, value: assessmentMap3B },
  { x: 2, y: 2, value: assessmentMap3C },
  { x: 2, y: 3, value: assessmentMap3D },
  { x: 2, y: 4, value: assessmentMap3E },
  { x: 3, y: 0, value: assessmentMap4A },
  { x: 3, y: 1, value: assessmentMap4B },
  { x: 3, y: 2, value: assessmentMap4C },
  { x: 3, y: 3, value: assessmentMap4D },
  { x: 3, y: 4, value: assessmentMap4E },
  { x: 4, y: 0, value: assessmentMap5A },
  { x: 4, y: 1, value: assessmentMap5B },
  { x: 4, y: 2, value: assessmentMap5C },
  { x: 4, y: 3, value: assessmentMap5D },
  { x: 4, y: 4, value: assessmentMap5E },
];

const heatmapYMap = {
  A: 0,
  B: 1,
  C: 2,
  D: 3,
  E: 4,
};

$(function () {
  $("#risk-heatmap").highcharts({
    chart: {
      type: "heatmap",
      marginTop: 40,
      marginBottom: 60,
      plotBackgroundColor: "none",
      events: {
        load: function () {
          var points = this.series[0].data,
            lenY = this.yAxis[0].tickPositions.length - 1,
            lenX = this.xAxis[0].tickPositions.length - 1,
            x = lenX,
            tmpX = 0,
            y = 0,
            j = 0;

          $.each(points, function (i, p) {
            if (
              (p.x == 0 && p.y == 0) ||
              (p.x == 0 && p.y == 1) ||
              (p.x == 1 && p.y == 0)
            ) {
              p.update(
                {
                  color: "#81B014",
                },
                false
              );
            } else if (
              (p.x == 0 && p.y == 2) ||
              (p.x == 1 && p.y == 1) ||
              (p.x == 2 && p.y == 0)
            ) {
              p.update(
                {
                  color: "green",
                },
                false
              );
            } else if (
              (p.x == 0 && p.y == 3) ||
              (p.x == 1 && p.y == 2) ||
              (p.x == 2 && p.y == 2) ||
              (p.x == 2 && p.y == 1) ||
              (p.x == 3 && p.y == 0)
            ) {
              p.update(
                {
                  color: "yellow",
                },
                false
              );
            } else if (
              (p.x == 4 && p.y == 0) ||
              (p.x == 4 && p.y == 1) ||
              (p.x == 3 && p.y == 1) ||
              (p.x == 3 && p.y == 2) ||
              (p.x == 2 && p.y == 3) ||
              (p.x == 1 && p.y == 3) ||
              (p.x == 1 && p.y == 4) ||
              (p.x == 0 && p.y == 4)
            ) {
              p.update(
                {
                  color: "orange",
                },
                false
              );
            } else {
              p.update(
                {
                  color: "red",
                },
                false
              );
            }
          });
          this.isDirty = true;
          this.redraw();
        },
      },
    },
    plotOptions: {
      heatmap: {
        borderWidth: 0.5,
        borderColor: "#333",
      },
    },

    title: null,

    xAxis: {
      categories: ["1", "2", "3", "4", "5"],
      title: {
        text: "Likelihood of Failure",
      },
    },
    yAxis: {
      categories: ["A", "B", "C", "D", "E"],
      title: {
        enabled: true,
        text: "Consequences of Failure",
        align: "middle", // Align the title to the top of the axis
        offset: 0, // Adjust offset if needed
        x: -40,
        y: 0,
        useHTML: true,
      },
    },

    colorAxis: {
      min: 0,
      minColor: "transparent",
      maxColor: "transparent",
      //maxColor: Highcharts.getOptions().colors[0]
    },

    legend: {
      enabled: false,
      align: "right",
      layout: "vertical",
      margin: 0,
      verticalAlign: "top",
      y: 25,
      symbolHeight: 320,
    },
    tooltip: {
      formatter: function () {
        return (
          this.series.xAxis.categories[this.point.x] +
          this.series.yAxis.categories[this.point.y] +
          ": " +
          Number(this.point.value)
        );
      },
    },
    /*want to make this part dynamically populated*/
    series: [
      {
        data: structuredClone(rawHeatmapData),
        dataLabels: {
          enabled: true,
          color: "black",
          style: {
            textShadow: "none",
          },
        },
      },
    ],
  });

  var jsonAssessmentPieChart = JSON.parse(jsonAssessmentPieChartVar);
  const pieChartData = [];
  Object.keys(jsonAssessmentPieChart).forEach((key) => {
    var value = parseInt(jsonAssessmentPieChart[key]);
    if (isNaN(value)) {
      value = 0; // or any other default integer value
    }
    pieChartData.push({
      name: key,
      y: value,
    });
  });
  $("#risk-piechart").highcharts({
    chart: {
      type: "pie",
      spacing: [0, 0, 0, 0],
    },
    title: {
      text: "",
    },
    tooltip: {},
    plotOptions: {
      pie: {
        size: "100%",
        colors: ["#81B014", "green", "yellow", "orange", "red"],
      },
      series: {
        allowPointSelect: true,
        cursor: "pointer",
        dataLabels: [
          {
            enabled: false,
            distance: 20,
          },
          {
            enabled: true,
            distance: -40,
            format: "{point.y}",
            style: {
              fontSize: "1.2em",
              textOutline: "none",
              opacity: 0.7,
            },
            filter: {
              operator: ">",
              property: "total",
              value: 10,
            },
          },
        ],
        showInLegend: true,
      },
    },
    series: [
      {
        name: "Total",
        data: pieChartData,
      },
    ],
  });

  // Bar Chart
  var jsonAssessmentBarChart = JSON.parse(jsonAssessmentBarChartVar);
  // console.log(jsonAssessmentBarChart, 'bar chart')
  const barSeriesData = {
    "Very Low": {
      name: "Very Low",
      data: [],
    },
    Low: {
      name: "Low",
      data: [],
    },
    Medium: {
      name: "Medium",
      data: [],
    },
    High: {
      name: "High",
      data: [],
    },
    "Very High": {
      name: "Very High",
      data: [],
    },
  };

  Object.values(jsonAssessmentBarChart).forEach((val, index) => {
    Object.keys(val).forEach((key) => {
      barSeriesData[key].data.push(Number(val[key]));
    });
  });

  $("#risk-barchart").highcharts({
    chart: {
      type: "column",
    },
    title: {
      text: "",
    },
    xAxis: {
      categories: Object.keys(jsonAssessmentBarChart),
      crosshair: true,
    },
    yAxis: {
      min: 0,
      tickInterval: 1,
    },
    tooltip: {},
    plotOptions: {
      column: {
        pointPadding: 0.2,
        borderWidth: 0,
      },
    },
    colors: ["#81B014", "green", "yellow", "orange", "red"],
    series: Object.values(barSeriesData),
  });
});
