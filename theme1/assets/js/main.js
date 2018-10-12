(function($) {
  'use strict';  
    /*======================================/
                  Preloader JS
    ======================================*/  
      var prealoaderOption = $(window);
      prealoaderOption.on("load", function () {
          var preloader = jQuery('.spinner');
          var preloaderArea = jQuery('.preloader-area');
          preloader.fadeOut();
          preloaderArea.delay(350).fadeOut('slow');
      });
    /*======================================/
                  Preloader JS
    ======================================*/
    /*======================================/
                sticky header JS
    ======================================*/
    $(window).on('scroll', function () {
        var scroll = $(window).scrollTop();
        if (scroll < 100) {
            $("#header-area").removeClass("sticky");
        } else {
            $("#header-area").addClass("sticky");
        }
    });
    /*======================================/
                sticky header JS
    ======================================*/
    /*======================================/
                  scroll top JS
    ======================================*/
    $("a.page-scroll").on('click', function (event) {
        if (this.hash !== "") {
            event.preventDefault();
            var hash = this.hash;
            //console.log($(hash).offset().top - topOffset);
            $('html, body').animate({
                scrollTop: $(hash).offset().top - $("header").outerHeight() + "px"
            }, 1200, function () {

                //window.location.hash = hash;
            });
        } // End if
    });
    /*======================================/
                  scroll top JS
    ======================================*/
    /*======================================/
              slick slider js
    ======================================*/
    $('.single-slide').owlCarousel({
        loop:true,
        margin:0,
        dots:false,
        nav:true,
        smartSpeed: 700,
        navText : ["<i class='fa fa-angle-left'></i>","<i class='fa fa-angle-right'></i>"],
        responsive:{
            0:{
                items:1
            },
            600:{
                items:1
            },
            1000:{
                items:1
            }
        }
    })
    $('.car-slide').owlCarousel({
        loop:true,
        margin:0,
        dots:false,
        nav:true,
        smartSpeed: 700,
        navText : ["<i class='fa fa-angle-left'></i>","<i class='fa fa-angle-right'></i>"],
        responsive:{
            0:{
                items:1
            },
            600:{
                items:1
            },
            1000:{
                items:1
            }
        }
    })
    $('.review-slide').owlCarousel({
        loop: true,
        margin: 10,
        responsiveClass:true,
        autoplay:false,
        dots:true,
        nav:false,
        autoplayTimeout: 4000,
        responsive:{
            0:{
                items:1,
            },
            600:{
                items:1,
            },
            1200:{
                items:1,
            }
        }
    })
    /*======================================/
                slick slider js
    ======================================*/
    /*======================================/
                Monthly js
    ======================================*/
    $(window).load( function() {
        $('#mycalendar2').monthly({
          mode: 'picker',
          target: '#mytarget',
          setWidth: '250px',
          startHidden: true,
          showTrigger: '#mytarget',
          stylePast: true,
          disablePast: true
        });
        $('#mycalendar').monthly({
          mode: 'picker',
          target: '#mytarget2',
          setWidth: '250px',
          startHidden: true,
          showTrigger: '#mytarget2',
          stylePast: true,
          disablePast: true
        });
    });  
    /*======================================/
                Monthly js
    ======================================*/
    /*======================================/
                counterup JS
    ======================================*/
    $('.counter').counterUp({
        delay: 80,
        time: 8000
    });
    /*======================================/
                      counterup JS
    ======================================*/
    /*======================================
                    google map JS
    ======================================*/ 
        $(window).on('load', function initMap() {
            var map = new google.maps.Map(document.getElementById('map'), {
                center: {
                    lat: 23.810332,
                    lng: 90.412518
                },
                zoom: 13
            });
            // Let's also add a marker while we're at it
            var marker = new google.maps.Marker({
                position: new google.maps.LatLng(23.810332, 90.412518),
                map: map,
                icon: {
                    url: 'assets/img/marker.png',
                },
                animation: google.maps.Animation.BOUNCE
            });
        });
    /*======================================  
                    google map JS
    ======================================*/
    /*=======================
              Scroll top js
    =========================*/
        $(window).on('scroll', function() {
            if ($(this).scrollTop() > 100) {
                $('#scroll-up').fadeIn();
            } else {
                $('#scroll-up').fadeOut();
            }
        });
        $('#scroll-up').on('click', function() {
            $("html, body").animate({
                scrollTop: 0
            }, 600);
            return false;
        });
    /*=======================
              Scroll top js
    =========================*/
    
})(window.jQuery);   
   