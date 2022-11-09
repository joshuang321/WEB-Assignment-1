/****************************************************************************************
 * This javascript file is required for use by                                          *
 *                                                                                      *
 * 1) Login.cshtml                                                                      *
 * 2) LoginVerification.cshtml                                                          *
 * 3) LoginVerificationPromptEmail.cshtml                                               *
 * 4) SetNewPassword.cshtml                                                             *
 *                                                                                      *
 * Do not attempt to modify any part of this code,or even attempt to delete this js file*
 * unless its the author himself                                                        *
 ****************************************************************************************
 */ 

//Done by Zhe Yun
for (let index in $("div.codeBox").length) {
    $("div.codeBox").eq(i).on("click", () => {
        console.log("Click listener activated");
        $("div.codeBoxInternalDecorative").css({
            "animation-play-state": "running"
        });
    });
}

$("div.resettingPassword").eq(0).addEventListener("click", () => {
    $("div.resettingPassword").css({ "display": "flex" });
}); 

$("div#Customer").on("mouseover", () => {
    $("div#enlargeable-role-details-left-1").css({
        "width": "170px",
        "height": "165px",
        "opacity": "1"
    }); 
});

$("div#Customer").on("mouseleave", () => {
    $("div#enlargeable-role-details-left-1").css({
        "width": "0px",
        "height": "0px",
        "opacity": "0"
    });
}); 

$("div#ProductManager").on("mouseover", () => {
    $("div#enlargeable-role-details-right-1").css({
        "width": "170px",
        "height": "165px",
        "opacity": "1"
    });
});

$("div#ProductManager").on("mouseleave", () => {
    $("div#enlargeable-role-details-right-1").css({
        "width": "0px",
        "height": "0px",
        "opacity": "0"
    });
});

$("div#SalesPersonnel").on("mouseover", () => {
    $("div#enlargeable-role-details-left-2").css({
        "width": "170px",
        "height": "165px",
        "opacity": "1"
    });
});

$("div#SalesPersonnel").on("mouseleave", () => {
    $("div#enlargeable-role-details-left-2").css({
        "width": "0px",
        "height": "0px",
        "opacity": "0"
    });
});

$("div#MarketingManager").on("mouseover", () => {
    $("div#enlargeable-role-details-right-2").css({
        "width": "170px",
        "height": "165px",
        "opacity": "1"
    });
});

$("div#MarketingManager").on("mouseleave", () => {
    $("div#enlargeable-role-details-right-2").css({
        "width": "0px",
        "height": "0px",
        "opacity": "0"
    });
});
