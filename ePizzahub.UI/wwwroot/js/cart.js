

function AddToCart(ItemId, UnitPrice, Quantity) {
    $.ajax({
        type: "GET",
        url: "/Cart/AddToCart/" + ItemId + "/" + UnitPrice + "/" + Quantity,
        success: function (response) {
            $("#cartCounter").text(response.count)
        },
        error: function (event) {
            alert("Error in adding item. Please try again")
        }
    })
}