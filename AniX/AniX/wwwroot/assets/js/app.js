// Template Name: AnimeLoop
// Template URL: https://techpedia.co.uk/AnimeLoop/
// Description: AnimeLoop HTML5 Template
// Author: TPD-Themes
// Version: 1.0.0
(function (window, document, $, undefined) {
  "use strict";
  var animeInit = {
    i: function (e) {
      animeInit.s();
      animeInit.methods();
    },
    s: function (e) {
      (this._window = $(window)),
        (this._document = $(document)),
        (this._body = $("body")),
        (this._html = $("html"));
    },
    methods: function (e) {
      animeInit.w();
      animeInit.hidepreloader();
      animeInit.animeBackToTop();
      animeInit.passwordHide();
      animeInit.intializeSlick();
      animeInit.countdownInit(".countdown", "2023/12/01");
      animeInit.salActivation();
      animeInit.policies();
      animeInit.weeklyScheduleNav();
      animeInit.videplay();
      animeInit.continueEmail();
      animeInit.trailerModel();
    },
    w: function (e) {
      this._window.on("load", animeInit.l).on("scroll", animeInit.res);
    },
    hidepreloader: function () {
      $('#preloader').hide();
    },
    animeBackToTop: function () {
      var btn = $("#backto-top");
      $(window).on("scroll", function () {
        if ($(window).scrollTop() > 300) {
          btn.addClass("show");
        } else {
          btn.removeClass("show");
        }
      });
      btn.on("click", function (e) {
        e.preventDefault();
        $("html, body").animate(
          {
            scrollTop: 0,
          },
          "300"
        );
      });
    },
    policies: function () {
      if ($('.policy').length) {
        $('.policy-item').on('click', function () {
          var count = $(this).data('count');
          $('.policy-item').removeClass('show');
          $('.policy-content').hide('slow');
          $('.policy-item p').hide('slow');
          $('.policy-item .minus').hide();
          $('.policy-item .plus').show();
          $(this).addClass('show');
          $('div.s-' + count).show('slow');
          $('div.s-' + count + ' p').show('slow');
          $('div.s-' + count + ' .plus').hide();
          $('div.s-' + count + ' .minus').show();

        });
      }
    },
    passwordHide: function () {
      $(".toggle-password").click(function() {
        $(this).toggleClass("fa-eye fa-eye-slash");
        var input = $($(this).attr("toggle"));
        if (input.attr("type") == "password") {
          input.attr("type", "text");
        } else {
          input.attr("type", "password");
        }
      });
    },
    countdownInit: function (countdownSelector, countdownTime) {
      var eventCounter = $(countdownSelector);
      if (eventCounter.length) {
        eventCounter.countdown(countdownTime, function (e) {
          $(this).html(
            e.strftime(
              " <li>%D<small>d</small></li>\
                            <li>%H<small>h</small></li>\
                            <li>%M<small>m</small></li>\
                            <li>%S<small>s</small></li>"
            )
          );
        });
      }
    },
    salActivation: function () {
      sal({
        threshold: 0.1,
        once: true,
      });
    },
    intializeSlick: function (e) {
      if ($(".banner-slider").length) {
        $(".banner-slider").slick({
          infinite: true,
          arrows: true,
          slidesToShow: 1,
          slidesToScroll: 1,
          autoplay: true,
          autoplaySpeed: 3000,
          fade: true,
          responsive: [
            {
              breakpoint: 992,
              settings: {
                arrows: false,
              },
            },
          ]
        });
      }
      if ($(".date-slider").length) {
        $(".date-slider").slick({
          infinite: true,
          arrows: true,
          slidesToShow: 7,
          slidesToScroll: 1,
          autoplay: false,
          autoplaySpeed: 2000,
          responsive: [
            {
              breakpoint: 991,
              settings: {
                arrows: true,
                slidesToShow: 5,
              },
            },
            {
              breakpoint: 768,
              settings: {
                arrows: true,
                slidesToShow: 4,
              },
            },
            {
              breakpoint: 480,
              settings: {
                arrows: true,
                slidesToShow: 3,
              },
            },
          ],
        });
      }
    },
    videplay: function () {
      $(".video .play-btn").on("click", function () {
        $(".video .img-box").hide("slow");
        $(".video .video-box").show("slow");
      });
    },
    weeklyScheduleNav: function () {
      var el = $('.date-slider .nav-item a');
      if (el.length) {
        el.on("click", function () {
          $('.date-slider .nav-item .nav-link').removeClass('active');
          $(this).addClass('active');
        });
      }
    },
    continueEmail: function () {
      $('#continue-email').on('click',function(){
        $('.hide-link').hide()
        $('.login-sec').show()
      })
    },
    trailerModel:function(){
      $('#videoModal').on('hidden.bs.modal', function () {
        $("#videoModal iframe").attr("src", $("#videoModal iframe").attr("src"));
    });
    }
  };
  animeInit.i();
})(window, document, jQuery);
