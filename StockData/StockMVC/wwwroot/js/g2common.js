function addG2Stack(containerId, flattenRecords, key) {
    var chart = new G2.Chart({
        id: containerId,
        width: document.getElementById(containerId).clientWidth,
        height: 400,
        plotCfg: {
            //margin: [30, 0, 30, 0]
        }
    });

    var defs = {
        Date: {
            type: "timeCat",
            mask: "yyyy-mm-dd"
        },
        //Open: {
        //    type: "linear",
        //    min: 40,
        //    max: 700
        //}
    }

    chart.source(flattenRecords, defs);

    chart.area("stack").position("Date*" + key).color("Key");

    chart.render();
}

function addG2Path(containerId, flattenRecord, symbol1, symbol2, key) {
    var chart = new G2.Chart({
        id: containerId,
        width: document.getElementById(containerId).clientWidth,
        height: 400
    });

    var defs = {
        Date: {
            type: "timeCat",
            mask: "yyyy-mm-dd"
        }
    };

    filteredData = [];
    filteredDataDict = {};
    flattenRecord.forEach(function (d) {
        if (d.Key == symbol1 || d.Key == symbol2) {
            if (!(d.Date in filteredDataDict))
                filteredDataDict[d.Date] = {};
            filteredDataDict[d.Date][d.Key] = d[key];
        }
    });
    for (var key in filteredDataDict) {
        var item = {
            "Date": key
        };
        item[symbol1] = filteredDataDict[key][symbol1];
        item[symbol2] = filteredDataDict[key][symbol2];
        filteredData.push(item);
    }

    chart.source(filteredData, defs);
    chart.path().position(symbol1 + "*" + symbol2)
        .tooltip("Date*" + symbol1 + "*" + symbol2);
    chart.point().position(symbol1 + "*" + symbol2)
        .shape("triangle")
        .tooltip("Date*" + symbol1 + "*" + symbol2);
    chart.render();
}

function addG2KLine(containerId, flattenRecord, symbol) {
    var record = [];
    flattenRecord.forEach(function (d) {
        if (d.Key == symbol) record.push(d);
    });

    var chart = new G2.Chart({
        id: containerId,
        width: document.getElementById(containerId).clientWidth,
        height: 400
    });

    var frame = new G2.Frame(record);
    frame.addCol("Trend", function (d) {
        return (d.Open <= d.Close) ? 0 : 1;
    });

    var defs = {
        Trend: {
            type: "cat",
            values: ['raising', 'falling']
        },
        Date: {
            type: "timeCat",
            mask: "yyyy-mm-dd"
        }
    };

    chart.source(frame, defs);
    chart.schema()
        .position("Date*(Open+Close+High+Low)")
        .color("Trend", ['#19B24B', '#C00000'])
        .shape("candle")
        .tooltip("Open*Close*High*Low*Volume");
    chart.render();
}

function addG2Compare(containerId, flattenRecord, symbol1, symbol2) {
    var container = d3.select("#" + containerId);
    var width = document.getElementById(containerId).clientWidth;
    container.append("div").attr("id", "range1").style("width", "100%").style("float", "left");
    container.append("div").attr("id", "c1").style("width", "49%").style("float", "left");
    container.append("div").attr("id", "c2").style("width", "49%").style("float", "right");
    container.append("div").attr("id", "range2").style("width", "100%").style("float", "left");

    var r1 = new G2.Plugin.range({
        id: "range1",
        width: width,
        height: 20,
        dim: 'Date'
    });
    var r2 = new G2.Plugin.range({
        id: "range2",
        width: width,
        height: 20,
        dim: 'Date'
    });
    var c1 = new G2.Chart({
        id: "c1",
        width: width / 2 - 2,
        height: 300
    });
    var c2 = new G2.Chart({
        id: "c2",
        width: width / 2 - 2,
        height: 300
    });

    var record1 = [], record2 = [];
    flattenRecord.forEach(function (d) {
        if (d.Key == symbol1) record1.push(JSON.parse(JSON.stringify(d)));
        if (d.Key == symbol2) record2.push(JSON.parse(JSON.stringify(d)));
    });
    record1.forEach(function (d) {
        d.Date = new Date(d.Date).getTime();
    });
    record2.forEach(function (d) {
        d.Date = new Date(d.Date).getTime();
    });

    var frame1 = new G2.Frame(record1);
    var frame2 = new G2.Frame(record2);
    frame1.addCol("Trend", function (obj) {
        return (obj.Open <= obj.Close) ? 0 : 1;
    });
    frame2.addCol("Trend", function (obj) {
        return (obj.Open <= obj.Close) ? 0 : 1;
    });

    var defs = {
        Trend: {
            type: "cat",
            values: ['raising', 'falling']
        },
        Date: {
            type: "timeCat",
            mask: "yyyy-mm-dd"
        }
    };

    c1.source(frame1, defs);
    c2.source(frame2, defs);
    c1.schema()
        .position("Date*(Open+Close+High+Low)")
        .color("Trend", ['#19B24B', '#C00000'])
        .shape("candle")
        .tooltip("Open*Close*High*Low*Volume");
    c2.schema()
        .position("Date*(Open+Close+High+Low)")
        .color("Trend", ['#19B24B', '#C00000'])
        .shape("candle")
        .tooltip("Open*Close*High*Low*Volume");

    r1.source(frame1);
    r1.link(c1);
    r1.render();
    r2.source(frame2);
    r2.link(c2);
    r2.render();
}