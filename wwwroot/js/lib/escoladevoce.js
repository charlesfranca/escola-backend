var escoladevoce = {};

var jcrop_api,
    boundx,
    boundy;

var api_x, api_y, api_w, api_h;

function updatePreview(c) {
    if (parseInt(c.w) > 0) {
        api_x = c.x;
        api_y = c.y;
        api_w = c.w;
        api_h = c.h;

        var rx = 100 / c.w;
        var ry = 100 / c.h;

        $(document)
            .find('img#preview')
            .css({
                width: Math.round(rx * boundx) + 'px',
                height: Math.round(ry * boundy) + 'px',
                marginLeft: '-' + Math.round(rx * c.x) + 'px',
                marginTop: '-' + Math.round(ry * c.y) + 'px'
            });
    }
};

function selected(c){
    if(c.w){
        $("input#x").val(parseInt(Math.round(api_x)));
        $("input#y").val(parseInt(Math.round(api_y)));
        $("input#w").val(parseInt(Math.round(api_w)));
        $("input#h").val(parseInt(Math.round(api_h)));

        console.log(x);
        console.log(y);
        console.log(w);
        console.log(h);
    }

    updatePreview(c);
}

escoladevoce.openimagetocrop = function (inputFile) {
    var fileReader = new FileReader();
    var $input = $(inputFile);

    fileReader.onload = function (fileLoadedEvent) {

        // Create variables (in this scope) to hold the API and image size
        var img = document.createElement("img");
        img.src = fileLoadedEvent.target.result;
        var $img = $(img);
        $img.attr({id: "target", class: ""}).css({
            "max-width": "100%"
        });

        var imgPreview = document.createElement("img");
        imgPreview.src = fileLoadedEvent.target.result;
        var $imgPreview = $(imgPreview);
        $imgPreview
            .attr({id: "preview", class: "jcrop-preview"})
            .css({width: "100px", height: "100px", overflow: "hidden"})

        $('#myModal .modal-body img#target').replaceWith($img);
        $('#myModal .modal-body img#preview').replaceWith($imgPreview);

        $('#target').Jcrop({
            onChange: updatePreview,
            onSelect: selected,
            aspectRatio: 1,
            boxWidth: 400
        }, function () {
            // Use the API to get the real image size
            var bounds = this.getBounds();
            boundx = bounds[0];
            boundy = bounds[1];
            // Store the API in the jcrop_api variable
            jcrop_api = this;
        });
        $('#myModal').modal('show');
    };

    var file = inputFile.files[0];
    fileReader.readAsDataURL(file);
}