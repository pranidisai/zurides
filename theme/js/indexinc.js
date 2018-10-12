function increaseValue(e) {
    
    var elimentid = "Qty_" + e;
    var value = parseInt(document.getElementById(elimentid).value, 10);
  value = isNaN(value) ? 0 : value;
  value++;
  var CouponId = e;
  var presentQunatiy = value;
  //  var data = { Id: CouponId, Qunatity: Qnty };
  // alert(data);
  
  var limit = "Limit_" + e;
  var limitvalue = parseInt(document.getElementById(limit).value, 10);
  //caluclation part
  var fairelement = "fair_" + e;
  var twoway = "twowayfair_"+e;
  var rent = "RentFair_"+e;
  var fairamount = 0;
  var xval = "mySelect_" + e;
  var x = parseInt(document.getElementById(xval).value, 10);
  if (x >0) {
     
      if (value <= limitvalue) {
          document.getElementById(elimentid).value = value;
          if (x == 1) {
              fairamount= parseInt(document.getElementById(fairelement).innerHTML, 10);
          }
          else if (x == 2) {
              fairamount = parseInt(document.getElementById(twoway).value, 10);
          }
          else if (x == 3) {
              fairamount = parseInt(document.getElementById(rent).value, 10);
          }
          var basicfair = value * (fairamount);
          var gst = value * (fairamount * 0.05);
          var tax = value * (fairamount * 0.05);
          var netapay = value * fairamount + gst + tax;
          document.getElementById("BasicFair_" + e).innerHTML = basicfair;
          document.getElementById("GST_" + e).innerHTML = gst;
          document.getElementById("Svtx_" + e).innerHTML = tax;
          document.getElementById("Netpay_" + e).innerHTML = netapay

          document.getElementById("TBasicFair_" + e).value = basicfair;
          document.getElementById("TGST_" + e).value = gst;
          document.getElementById("TSvtx_" + e).value = tax;
          document.getElementById("TNetpay_" + e).value = netapay;
      }
      else {
          alert("Requested Qunatity is not available ");
      }
  }
  else {
      alert("Please select Jouney Type");
  }
 
  //$.ajax({
   
  //    type: "POST",
  //    url: "/Cart/Updatecart",
  //    dataType: "JSON",
  //    data: { Cid: CouponId, Operation: presentQunatiy },
  //    success: function (Data) {
  //        window.location.reload();
  //        alert(Data);
  //    },
  //    error: function (XMLHttpRequest, textStatus, errorThrown) {
  //        //alert('Oh no :(' + XMLHttpRequest + textStatus + errorThrown);
  //    }
  //});

   
  //  var data = { Id: CouponId, Qunatity: Qnty };
  // alert(data);

 

}

function CallChangefunc(e)
{
    alert(this.value);
}
function selectvaluechnage(e) {
    
    var x = parseInt(document.getElementById("mySelect_" + e).value, 10);
    document.getElementById("BasicFair_" + e).innerHTML = 0;
    document.getElementById("GST_" + e).innerHTML = 0;
    document.getElementById("Svtx_" + e).innerHTML = 0;
    document.getElementById("Netpay_" + e).innerHTML = 0
    document.getElementById("Qty_" + e).value = 0;
    document.getElementById("TBasicFair_" + e).value = 0;
    document.getElementById("TGST_" + e).value = 0;
    document.getElementById("TSvtx_" + e).value = 0;
    document.getElementById("TNetpay_" + e).value = 0;
  
}
function ConfirmBooking(e) {
    var elimentid = "Qty_" + e;
    var value = parseInt(document.getElementById(elimentid).value, 10);
  
    if (value > 0) {

        var formelement = "post1" + e;
        // document.getElementById(formelement).submit();
        var dataToPost = $(formelement).serialize()
        $.post("Search/Verify", dataToPost)
            .done(function (response) {
                alert(response);
                // this is the "success" callback
            })
            .fail(function (jqxhr, status, error) {
                // this is the ""error"" callback
            })


        var x = confirm("Are you sure you want to Confirm?");
        if (x == true) {


          
            var presentQunatiy = value;
            var formelement = "post" + e;
          
            //caluclation part
            var fairelement = "fair_" + e
            var fairamount = parseInt(document.getElementById(fairelement).innerHTML, 10);
            var basicfair = value * (fairamount);
            var gst = value * (fairamount * 0.05);
            var tax = value * (fairamount * 0.05);

            var source = document.getElementById(elimentid).value;
            var destination = document.getElementById(elimentid).value;
            var bookingType = document.getElementById(elimentid).value
            var netapay = value * fairamount + gst + tax;
        } else {
           
        }
    }
    else {
        alert("Please select the seats ");
    }
    
    
    

}
function decreaseValue(e) {
    var elimentid = "Qty_" + e;
    var value = parseInt(document.getElementById(elimentid).value, 10);
  value = isNaN(value) ? 0 : value;
  value < 1 ? value = 1 : '';
  value--;
  var limit = "Limit_" + e;
  var limitvalue = parseInt(document.getElementById(limit).value, 10);
  //caluclation part
  var fairelement = "fair_" + e
  var fairamount = parseInt(document.getElementById(fairelement).innerHTML, 10);
  var basicfair = value * (fairamount);
  var gst = value * (fairamount * 0.05);
  var tax = value * (fairamount * 0.05);
  var netapay = (value * fairamount )+ gst + tax;
  if (value <= limitvalue) {
      document.getElementById(elimentid).value = value;
      document.getElementById("BasicFair_" + e).innerHTML = basicfair;
      document.getElementById("GST_" + e).innerHTML = gst;
      document.getElementById("Svtx_" + e).innerHTML = tax;
      document.getElementById("Netpay_" + e).innerHTML = netapay;
      document.getElementById("TBasicFair_" + e).value = basicfair;
      document.getElementById("TGST_" + e).value = gst;
      document.getElementById("TSvtx_" + e).value = tax;
      document.getElementById("TNetpay_" + e).value = netapay;
  }
    
  
}