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