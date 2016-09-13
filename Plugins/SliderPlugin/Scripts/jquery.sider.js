jQuery(document).ready(function () {
    if (jQuery.fn.cssOriginal !== undefined)   // CHECK IF fn.css already extended
        jQuery.fn.css = jQuery.fn.cssOriginal;

    jQuery('.fullwidthbanner').revolution(
            {
                delay: 5000,
                startheight: 413,
                startwidth: 940,
                hideThumbs: 200,
                thumbWidth: 100,
                thumbHeight: 50,
                thumbAmount: 5,
                navigationType: "none",
                navigationArrows: "verticalcentered",
                navigationStyle: "round",
                navigationHAlign: "right",
                navigationVAlign: "top",
                navigationHOffset: 0,
                navigationVOffset: 20,
                soloArrowLeftHalign: "left",
                soloArrowLeftValign: "center",
                soloArrowLeftHOffset: 20,
                soloArrowLeftVOffset: 0,
                soloArrowRightHalign: "right",
                soloArrowRightValign: "center",
                soloArrowRightHOffset: 20,
                soloArrowRightVOffset: 0,
                touchenabled: "on",
                onHoverStop: "on",
                navOffsetHorizontal: 0,
                navOffsetVertical: 20,
                hideCaptionAtLimit: 0,
                hideAllCaptionAtLilmit: 0,
                hideSliderAtLimit: 0,
                stopAtSlide: -1,
                stopAfterLoops: -1,
                shadow: 0,
                fullWidth: "on"
            });
});