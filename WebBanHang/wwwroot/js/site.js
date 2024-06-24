// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
$(document).ready(function () {
    showQuantityCart();
    $('#btnSearch').click(function () {
        var phone = $('#phoneInput').val();
        $.ajax({
            url: '/Admin/ManagementOrder/SearchPhoneAPI',
            type: 'GET',
            data: { Phone: phone },
            success: function (data) {
                if (data.msg == "Product added to cart") {
                    var orders = data.orders; // Assuming your response contains an array of orders
                    console.log(orders);
                    var table = $('#ordersTable tbody');
                    table.empty(); // clear the table before adding new result
                    $('#ordersTable').removeClass('d-none');
                    $('#Message').addClass('d-none');

                    // Use jQuery's $.each() to iterate over the orders array
                    $.each(orders, function (i, order) {
                        // add each order to the table
                        var row = "<tr>" +
                            "<td>" + order.id + "</td>" +
                            "<td>" + formatDate(order.orderDate) + "</td>" +
                            "<td>" + order.customerName + "</td>" +
                            "<td>" + order.address + "</td>" +
                            "<td>" + order.phone + "</td>" +
                            "<td>" + order.total + "</td>" +
                            "<td>" + order.state + "</td>" +
                            "</tr>";

                        table.append(row);
                    });
                } else if (data.msg == "error") {
                    // handle error
                    $('#Message').removeClass('d-none');
                    $('#ordersTable').addClass('d-none');
                }
            },
            error: function (jqXHR, textStatus, errorThrown) {
                console.log(textStatus, errorThrown);
            }
        });
    });
    $('.select2').select2({
        tags: true
    });
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

$(".btnUpdate").click(function (evt) {
    evt.preventDefault();
    alert("tesst");
    let id = $(this).attr("data-productID")
    let qty = $(this).closest("form").find(".qty").val();
    $.ajax({
        url: "/Customer/Cart/UpdateToCartAPI",
        data: { "productID": id, "qty":qty },
        success: function (data) {
            Swal.fire({
                title: data.msg,
                text: "Cập nhật sản phẩm thành công",
                icon: "success"
            });
            showQuantityCart();
            showTotalPrice();
        }
    })

})

$('#CategoryId').change(function () {
    var categoryId = $(this).val();
    $.ajax({
        url: "/Admin/Product/GetCompanies",
        data: { CategoryId: categoryId },
        success: function (data) {
            var brandsSelect = $('#Companies');
            console.log("1: ", brandsSelect)
            brandsSelect.empty();
            $.each(data, function (index, brand) {
                brandsSelect.append('<option value="' + brand.id + '">' + brand.name + '</option>');
                console.log("2: ", brand)

            });
        }
    });
});

//$(".btnremove").click(function (evt) {
//    evt.preventDefault();
//    let id = $(this).attr("data-productID")
//    $.ajax({
//        url: "/Customer/Cart/DeleteAPI",
//        data: { "productID": id},
//        success: function (data) {
//            Swal.fire({
//                title: data.msg,
//                text: "Xoá sản phẩm thành công",
//                icon: "success"
//            });
//            showTotalPrice();
//        }
//    })
//})


let showTotalPrice = () => {
    $.ajax({
        url: "/Customer/Cart/GetTotalPrice",
        success: function (data) {
            let num = new Intl.NumberFormat('ja-JP', { style: 'currency', currency: 'USD' }).format(
                data.total,
            )
            $(".totalprice").text(num);
        }
    })

}

let showQuantityCart = () => {
    $.ajax({
        url: "/Customer/Cart/GetQuantity",
        success: function (data) {        
            $(".showcart").text(data.qty)
        }
    })
   // alert(id);
}

function formatDate(date) {
    var d = new Date(date), // Chuyển string date thành object
        month = '' + (d.getMonth() + 1), // Lấy tháng và cộng 1 vì tháng bắt đầu từ 0
        day = '' + d.getDate(), // Lấy ngày
        year = d.getFullYear(); // Lấy năm

    // Nếu ngày và tháng có 1 chữ số, thêm số 0 vào đầu.
    if (month.length < 2)
        month = '0' + month;
    if (day.length < 2)
        day = '0' + day;

    // Trả về string theo định dạng mong muốn
    return [day, month, year].join('/');
}