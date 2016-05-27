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