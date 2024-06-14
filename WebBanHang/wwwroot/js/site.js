// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
$(document).ready(function () {
    showQuantityCart();
})
$(".addtocart").click(function (evt) {
    evt.preventDefault();
    let id = $(this).attr("data-productID")
    $.ajax({
        url: "/Customer/Cart/AddToCartAPI",
        data: { "productID": id },
        success: function (data) {
            Swal.fire({
                title: data.msg,
                text: "You clicked the button",
                icon: "success"
            });
            showQuantityCart();
        }
    })
   // alert(id);

})

let showQuantityCart = () => {
    $.ajax({
        url: "/Customer/Cart/GetQuantity",
        success: function (data) {        
            $(".showcart").text(data.qty)
        }
    })
   // alert(id);
}