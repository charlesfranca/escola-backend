var escoladevoce = {};

escoladevoce.openimagetocrop = function(inputFile) {
    var fileReader = new FileReader();
    var $input = $(inputFile);
    
    fileReader.onload = function(fileLoadedEvent) {
        var image = $($input.data("imagetochange"));
        image.attr("src", "");
        image.attr("src", fileLoadedEvent.target.result);
        console.log(fileLoadedEvent.target);
        $('#profilePhoto').cropper({
            aspectRatio: 1 / 1,
            crop: function(e) {
                // Output the result data for cropping image.
                console.log(e.x);
                console.log(e.y);
                console.log(e.width);
                console.log(e.height);
                console.log(e.rotate);
                console.log(e.scaleX);
                console.log(e.scaleY);
            }
        });
        $('#myModal').modal('show')
    };

    var file = inputFile.files[0];
    fileReader.readAsDataURL(file);
}