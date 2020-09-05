// croll Reveal Animations

var fromBottomAnimation = {
  distance: '150px',
  origin: 'bottom',
  duration: 600,
  easing: 'cubic-bezier(0.785, 0.135, 0.15, 0.86)',
  rotate: {
    x: 20,
    y: 30,
    z: 0
  },
  scale: 0.8,
  opacity: 0,
  interval: 150
};
var fromRightAnimation = {
  distance: '250px',
  origin: 'right',
  duration: 600,
  easing: 'cubic-bezier(0.785, 0.135, 0.15, 0.86)',
  rotate: {
    x: 20,
    y: 30,
    z: 0
  },
  scale: 0.8,
  opacity: 0,
  interval: 150,
  delay: 100
};
var fromLeftAnimation = {
  distance: '250px',
  origin: 'left',
  duration: 600,
  easing: 'cubic-bezier(0.785, 0.135, 0.15, 0.86)',
  rotate: {
    x: 20,
    y: 30,
    z: 0
  },
  scale: 0.3,
  opacity: 0,
  interval: 150,
  delay: 100
};

// Initilize head section animations

ScrollReveal().reveal(
  '.head-section-leftside .links-head, .head-section-rightside',
  fromBottomAnimation
);
ScrollReveal().reveal('.section-head .leftside-image', fromLeftAnimation);

// Initilize magazine section animations

ScrollReveal().reveal(
  '.section-magazine .section-title-container, .section-magazine .text-with-scroll, .section-magazine .magazine-image-container',
  fromBottomAnimation
);

// Initilize offers section animations

ScrollReveal().reveal(
  '.main-content .offers-section .rightside-image, .offers-section .section-title-container.left',
  fromRightAnimation
);
ScrollReveal().reveal('.offers-slider-container', fromBottomAnimation);

// Initilize article section animations

ScrollReveal().reveal(
  '.articles-section .articles-slider, .articles-section .article-slider-navigation-container',
  fromBottomAnimation
);

// Initilize magazines archive section animations

ScrollReveal().reveal(
  '.magazine-archive-section .rightside-image',
  fromRightAnimation
);
ScrollReveal().reveal(
  '.magazine-archive-section .section-title-container.red',
  fromLeftAnimation
);
ScrollReveal().reveal(
  '.magazine-archive-section .magazine-archive-slider-container .magazine-archive-slider, .magazine-archive-section .magazine-archive-slider-container .shapebg,.magazine-archive-section .magazine-archive-slider-container .numeral-slider-navigation-container',
  fromBottomAnimation
);

// Initilize customers club section animations

ScrollReveal().reveal(
  '.customers-club-section .section-title-container.purple',
  fromBottomAnimation
);
ScrollReveal().reveal(
  '.customers-club-section .rightside-image, .customers-club-section .links-container',
  fromRightAnimation
);
ScrollReveal().reveal(
  '.customers-club-section .login-container',
  fromBottomAnimation
);

// Offers section slider

var offersSlider = new Swiper('.offers-slider', {
  slidesPerView: 4,
  grabCursor: true,
  navigation: {
    nextEl: '.offers-slide-button-next',
    prevEl: '.offers-slide-button-prev'
  },
  breakpoints: {
    0: {
      slidesPerView: 1
    },
    768: {
      slidesPerView: 2
    },
    992: {
      slidesPerView: 4
    }
  }
});

// Each offer item has an slider in it, so we use jQuery each to create a new instance for each of them

$('.offers-text-slider').each(function(index) {
  $(this).addClass(`instance-${index}`);
  $(this)
    .siblings('.offers-text-slider-pagination')
    .addClass(`instance-pagination-${index}`);
  var newInstance = new Swiper(`.instance-${index}`, {
    slidesPerView: 1,
    touchRatio: 0,
    pagination: {
      el: `.instance-pagination-${index}`,
      clickable: true
    }
  });
});

var articlesSlider = new Swiper('.articles-slider', {
  slidesPerView: 4,
  spaceBetween: 40,
  grabCursor: true,
  loop: true,
  navigation: {
    nextEl: '.article-slide-button-next',
    prevEl: '.article-slide-button-prev'
  },
  pagination: {
    el: '.article-slider-pagination',
    clickable: true,
    renderBullet: function(index, className) {
      if (index > 9) {
        return `<span class="${className}">${index + 1}</span>`;
      } else {
        return `<span class="${className}">0${index + 1}</span>`;
      }
    }
  },
  breakpoints: {
    0: {
      slidesPerView: 1
    },
    768: {
      slidesPerView: 2
    },
    992: {
      slidesPerView: 3
    },
    1200: {
      slidesPerView: 4
    }
  }
});

var magazinesSlider = new Swiper('.magazine-archive-slider', {
  navigation: {
    nextEl: '.magazine-archive-slide-button-next',
    prevEl: '.magazine-archive-slide-button-prev'
  },
  pagination: {
    el: '.magazine-archive-slider-pagination',
    clickable: true,
    renderBullet: function(index, className) {
      if (index > 9) {
        return `<span class="${className}">${index + 1}</span>`;
      } else {
        return `<span class="${className}">0${index + 1}</span>`;
      }
    }
  },
  breakpoints: {
    0: {
      slidesPerView: 'auto',
      slidesPerColumn: 1,
      centeredSlides: true,
      pagination: false
    },
    992: {
      slidesPerView: 4,
      slidesPerColumn: 2,
      centeredSlides: false
    }
  }
});

// Converts svg img src to inline svg (So we can style it as we want with css (ex: Fill Color)
// https://stackoverflow.com/a/11978996

jQuery('img.svg-icon').each(function() {
  var $img = jQuery(this);
  var imgID = $img.attr('id');
  var imgClass = $img.attr('class');
  var imgURL = $img.attr('src');
  jQuery.get(
    imgURL,
    function(data) {
      var $svg = jQuery(data).find('svg');
      if (typeof imgID !== 'undefined') {
        $svg = $svg.attr('id', imgID);
      }
      if (typeof imgClass !== 'undefined') {
        $svg = $svg.attr('class', imgClass + ' replaced-svg');
      }
      $svg = $svg.removeAttr('xmlns:a');
      if (!$svg.attr('viewBox') && $svg.attr('height') && $svg.attr('width')) {
        $svg.attr(
          'viewBox',
          '0 0 ' + $svg.attr('height') + ' ' + $svg.attr('width')
        );
      }
      $img.replaceWith($svg);
    },
    'xml'
  );
});

// Particles

particlesJS.load('particles-js', 'js/particlesjs-config.json', function() {
  console.log('callback - particles.js config loaded');
});

// Menu mobile toggler

$('.menu-mobile-toggle, .menu-mobile-close').click(function() {
  $('.main-site-menu .menu').toggleClass('active');
});

// Adds dark class to menu when scrolled to bottom > 100px

$(window).scroll(function() {
  var scroll = $(window).scrollTop();
  if (scroll >= 100) {
    $('.site-header .main-site-menu').addClass('dark');
  }
  if (scroll <= 100) {
    $('.site-header .main-site-menu').removeClass('dark');
  }
});
