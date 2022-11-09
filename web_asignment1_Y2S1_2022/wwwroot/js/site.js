// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

//TRY NOT TO DO ANY FORM VALIDATION IN JAVASCRIPT
//TRY NOT TO DO ANY PAGE REDIRECTIONS IN JAVASCRIPT 
//ONLY USE JAVASCRIPT TO MANIPULATE CSS AND DOM ELEMENTS

//Done by Zhe Yun 

function signOutHandling() {
    const httpReq = new XMLHttpRequest();
    $("body").css({
        "overflow-y": "hidden",
        "opacity": 0
    });
    setTimeout(() => {
        $("body").css({ "display": "none" });
    }, 1750);
    setTimeout(() => {
        $("body").html(
            "<div class=\"matchParent flexBoxVertical gravity\">Please wait while we sign you out</div>"
        );
        $("body").css({ "display": "block"});
    }, 2000);
    setTimeout(() => {
        $("body").css({ "opacity": 1 }); 
    }, 2500); 
    setTimeout(() => {
        var url = window.location.href;
        if (url.toString().includes("https://localhost:")) {
            var port = url.split(':')[2].split('/')[0];
            httpReq.open("POST", `https://localhost:${port}/LoginCustomer/SignOut`, true);
            httpReq.setRequestHeader("Content-type", "application/json");
            httpReq.send(""); 
        } 
    }, 3000);
    setTimeout(() => {
        $("body").css({ "opacity": 0 });
    }, 4700)
    setTimeout(() => {
        $("body").css({ "display": "none" });
    }, 5500);
    setTimeout(() => {
        var url = window.location.href;
        var port = url.split(':')[2].split('/')[0];
        if (url.toString().includes("https://localhost:")) {
            window.location.href = `https://localhost:${port}/Home/Index`; 
        } 
    }, 6000);
}

function returnToPrevious() {
    $("body").css({
        "overflow-y": "hidden",
        "opacity": 0
    });
    setTimeout(() => {
        $("body").css({ "display": "none" });
    }, 1750);
    setTimeout(() => {
        var url = window.location.href;
        if (url.toString().includes("https://localhost:")) {
            var port = url.split(':')[2].split('/')[0];
            window.location.href = `https://localhost:${port}/Home/Index`;
        }
    }, 2500); 
}

$(document).ready(() => {
    //Add a click listener that would redirect the user to the login page..
    $("div.signInBar").on("click", () => {
        $("body").css({
            "overflow-y": "hidden",
            "opacity": 0
        });
        setTimeout(() => {
            $("body").css({ "display": "none" }); 
        }, 1750);
        setTimeout(() => {
            $("body").html("Please hang on while we sign you out...");
        }, 2000);
        setTimeout(() => {
            $("body").css({ "display": "block", "opacity": 1}); 
        }, 2250); 
        setTimeout(() => {
            //We can just use javascript to redirect to the previous page, if the user is not intending to sign out
            $("body").html(
                "<div class=\"matchParent flexBoxVertical gravity\">Press on the button below to continue sign out. If not, please click the 'Back' button to go back to your account<br /><br /><a onclick=\"signOutHandling()\" class=\"btn btn-primary\">Sign me out now</a><br /><br /><a onclick=\"returnToPrevious()\" class=\"btn btn-primary\">Go back</a></div>"
            )
        }, 2500); 
    });

    //Store the list of the images in a new array
    imageArr = new Array(
        "/Images/Model1.png",
        "/Images/Model2.png",
        "/Images/Model3.png",
        "/Images/0367420800_1_1_3.jpg",
        "/Images/0722437052_1_1_3.jpg",
        "/Images/1381043712_1_1_3.jpg",
        "/Images/2070239550_1_1_3.jpg",
        "/Images/2669795710_1_1_3.jpg",
        "/Images/2705273400_1_1_3.jpg",
        "/Images/5644031413_1_1_3.jpg",
        "/Images/6917485615_1_1_3.jpg",
        "/Images/9815401617_1_1_3.jpg"
    );

    //to div#ProductDescription
    productDescArr = new Array(
        "New Ladies Shirt!",
        "New T-shirt for gentlemen!",
        "New beach pants for gentlemen!",
        "New long-sleeved shirt and long pants for gentlemen!",
        "New T-shirt for gentlemen!",
        "",
        "",
        "",
        "",
        "",
        ""
    ); 

    for (let i = 0; i < imageArr.length; i++) {
        $("div#indicator").append("<div class=\"circle\" style=\"background-color: black; width:5px; height:5px\" id = \"dot\"></div><div class=\"whitespace\" style=\"width:5px; height:5px\" id = \"spacing\"></div>");
    }

    $("figure").mouseleave(() => {
        for (let i in $("div#dot")) {
            if ($("div#dot").eq(i).css("background-color") == "rgb(255, 0, 0)") {
                $("div#dot").eq(i).css({ "background-color": "red" });
            }
            else { //if the background-color of the $("div#dot") is not red then make the rest of the dots have a black colour
                $("div#dot").eq(i).css({ "background-color": "black" });
            }
        }
    });

    $("figure").mouseover(() => {
        for (let i in $("div#dot")) {
            if ($("div#dot").eq(i).css("background-color") == "rgb(255, 0, 0)") {
                $("div#dot").eq(i).css({ "background-color": "red" });
            }
            else { //if the background-color of the $("div#dot") is not red then make the rest of the dots have a black colour
                $("div#dot").eq(i).css({ "background-color": "white" });
            }
        }
    });

    //Initialize the coloured dot (Sorry my phrasing is kind of horrible)
    console.log($("div#dot").length) // 3 
    $("div#dot").eq(imageArr.indexOf($("img#image").attr("src"))).css({ "background-color": "red" });

    //Go to the next image DO NOT MODIFY THIS METHOD!!!
    $("button#next").click(() => {
        //initial config
        //animation-name: image-transition-to-left
        //animation-fill-mode: none
        //image will move from right to left, and once it moves to the extreme left and disappears pause the animation
        $("img#image").css({
            "animation-play-state": "running",
        });
        //change the animation-fill-mode to none and then change the animation name 
        setTimeout(() => {
            $("img#image").css({
                "animation-play-state": "paused",
                "animation-name": "image-transition-from-right"
            });
        }, 120);
        //change the image while the animation is still paused
        setTimeout(() => {
            console.log("Event listener has been triggered by the user");
            //Change the image description text accordingly 
            console.log($("div#ProductDescription").html())
            if (productDescArr.indexOf($("span#ItemHeader").html()) < productDescArr.length - 1) {
                $("span#ItemHeader").html(
                    productDescArr[productDescArr.indexOf($("span#ItemHeader").html()) + 1] 
                ); 
            }
            if (imageArr.indexOf($("img#image").attr("src")) < imageArr.length - 1) {
                //Change the alt text of the image too
                $("img#image").attr({
                    "src": imageArr[imageArr.indexOf($("img#image").attr("src")) + 1],
                    "alt": `Model${imageArr.indexOf($("img#image").attr("src")) + 1}`
                });
                //Change the dot color, while ensuring that the dot of the 
                for (let i in $("div#dot")) {
                    if ($("div#dot").eq(i).css("background-color") == "rgb(255, 0, 0)") { //If the background-color of the dot is red color
                        if (i + 1 < $("div#dot").length - 1) {
                            if (i - 1 > 0) {
                                if ($("div#dot").eq(i + 1).css("background-color") == "rgb(0, 0, 0)") { //If the dot next to the current dot is black color
                                    $("div#dot").eq(i).css({ "background-color": "black" });
                                }
                                else if ($("div#dot").eq(i + 1).css("background-color") == "rgb(255, 255, 255)") {

                                }
                            }
                        }
                        else { //if i + 1 already exceeds the last index
                            if (i - 1 > 0) {

                            }
                        }
                    }
                }
            }
            else { //given the fact that imageArr is an expandable array...
                $("img#image").attr({
                    "src": imageArr[0],
                    "alt": `Model${imageArr.indexOf(imageArr[0])}`
                });
                //Change the dot color
                for (let i in $("div#dot")) {

                }
            }
        }, 200);
        //once the image has been changed start the animation again... 
        setTimeout(() => {
            $("img#image").css({ "animation-play-state": "running" });
        }, 250);
        //the css configuration here must match the default configurations in the css
        setTimeout(() => {
            $("img#image").css({
                "animation-play-state": "paused",
                "animation-name": "image-transition-to-left"
            });
        }, 370);
    });

    //DO THE OPPOSITE (STILL WORKING ON THIS PART OF THE CODE  )
    $("button#previous").click(() => {
        $("img#image").css({
            "animation-fill-mode": "backwards",
            "animation-name": "image-transition-from-left",
            "animation-play-state": "running"
        });
        //Once 2 seconds is up it will stop the animation
        setTimeout(() => {
            $("img#image").css({
                "animation-play-state": "paused",
                "animation-fill-mode": "forwards",
                "animation-name": "image-transition-to-right"
            });
        }, 120);
        setTimeout(() => {
            if (imageArr.indexOf($("img#image").attr("src")) < imageArr.length - 1) {
                //Change the alt text of the image too
                $("img#image").attr({
                    "src": imageArr[imageArr.indexOf($("img#image").attr("src")) + 1],
                    "alt": `Model${imageArr.indexOf($("img#image").attr("src")) + 1}`
                });
            }
            else {
                $("img#image").attr({
                    "src": imageArr[0],
                    "alt": `Model${imageArr.indexOf(imageArr[0])}`
                });
            }
        }, 200);
        setTimeout(() => {
            $("img#image").css({ "animation-play-state": "running" }); //animation playing-style configured
        }, 250);
        setTimeout(() => {
            $("img#image").css({
                "animation-play-state": "paused",
                "animation-fill-mode": "backwards",
                "animation-name": "image-transition-from-left"
            });
            $(this).prop("disabled", false);
        }, 450);
    });
});
