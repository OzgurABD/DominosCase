$(document).ready(function () {
    CheckOrderButton();
    $("body").on("click", "#completeModal", function () {
        var CreateOrderModel = {
            CustomerName: $('#fnameModal').val(),
            AddressDetail: $('#addressModal').val(),
            ProductList: CartList,
        };
        $.ajax({
            type: "POST",
            dataType: "json",
            url: "http://localhost:52866/api/Order/OrderCreate",
            data: CreateOrderModel,
            //contentType: "application/json; charset=utf-8",
            success: function (result) {
                $("#completeOrder").modal('toggle');
                location.reload();
            }
        });
    });
});

var CartList = [];
function P_AddCart(id) {
    var IsGetList = false;
    var CartModel = {
        ID: 0,
        Name: "",
        Desc: "",
        Price: 0,
        Dprice: 0,
        TypeId: 0,
        Quantity: 0,
    };
    CartModel.ID = id;
    CartModel.Name = document.getElementById(id).getAttribute('data-pname');
    CartModel.Desc = document.getElementById(id).getAttribute('data-pdesc');
    CartModel.Price = document.getElementById(id).getAttribute('data-pprc');
    CartModel.Dprice = document.getElementById(id).getAttribute('data-pdprc');
    CartModel.TypeId = document.getElementById(id).getAttribute('data-ptype');
    CartModel.Quantity = 1;
    if (CartList.length === 0) {
        IsGetList = true;
        CartList[0] = CartModel;
        CheckOrderButton();
        $("#oCart").append('<tr id="potr' + CartModel.ID + '">>'
            + '<th><a href="#" onclick="Order_Delete(' + CartModel.ID + ')">Delete</a></th>'
            + '<th>'
            + CartModel.Name
            + '</th>'
            + '<th>'
            + CartModel.Desc
            + '</th>'
            + '<th>'
            + CartModel.Price
            + '</th>'
            + '<th id="poQ' + CartModel.ID + '">'
            + CartModel.Quantity
            + '</th>'
            + '</tr>');
    } else {
        CartList.forEach(function (element) {
            if (element.ID == CartModel.ID) {
                IsGetList = true;
                element.Quantity++;
                var q = "#" + "poQ" + id;
                var asa = parseInt($(q).html());
                $(q).html(element.Quantity);
            }
        });
    }
    if (IsGetList == false) {
        CartList.push(CartModel);
        CheckOrderButton();
        $("#oCart").append('<tr id="potr' + CartModel.ID + '">>'
            + '<th><a href="#" onclick="Order_Delete(' + CartModel.ID + ')">Delete</a></th>'
            + '<th>'
            + CartModel.Name
            + '</th>'
            + '<th>'
            + CartModel.Desc
            + '</th>'
            + '<th>'
            + CartModel.Price
            + '</th>'
            + '<th id="poQ' + CartModel.ID + '">'
            + CartModel.Quantity
            + '</th>'
            + '</tr>');
    }
}

function Order_Delete(id) {
    for (i = 0; i < CartList.length; i++) {
        if (CartList[i].ID === id) {
            CartList.splice(i, 1);
            var q = "#potr" + id;
            $(q).empty();
            $(q).remove();
            CheckOrderButton();
        }
    }
}

function CheckOrderButton() {
    if (CartList.length == 0) {
        $('#cob').attr("disabled", true);
    } else { $('#cob').attr("disabled", false); }
}

function P_Edit(id) {
    var s = "#" + id;
    var nm = $(s).find("#item_Name").val();
    var dsc = $(s).find("#item_Desc").val();
    var prc = parseFloat($(s).find("#item_Price").val());
    var dprc = parseFloat($(s).find("#item_DiscountPrice").val());
    //item_ProductTypeID

    $.ajax({
        type: "POST",
        dataType: "json",
        url: "http://localhost:52866/api/Order/ProductUpdate",
        data: { ID: id, Name: nm, Desc: dsc, Price: prc },
        //contentType: "application/json; charset=utf-8",
        //success: function (result) {
        //    alert(result);
        //}
    });
}