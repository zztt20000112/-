//index.js
//获取应用实例
const app = getApp();

Page({
  data: {
    imgUrls: [
      '/images/meishi1.jpg',
      '/images/meishi5.jpg',
      '/images/meishi3.jpg'
    ],
    indicatorDots: false,
    autoplay: true,
    interval: 2000,
    duration: 500,

    proList: [],
  },


  onLoad: function(option) {
    
  },

  onShow: function(e) {
    var that = this;
    let n;
    console.log(this);
    wx.request({
      url: 'http://localhost:54039/actionapi/Items/get',
      header: {
        'Content-Type': 'application/json'
      },
      method: 'GET',
      success: function (res) {
        for (n = res.data.length - 1; n >= 0; n--) {
          console.log("成功");
          app.data.id2 = res.data.length;
          console.log(app.data.id2);
          app.data.title = res.data[n].ItemsTitle;
          app.data.id = res.data[n].ItemsID;
          app.data.imagePath = res.data[n].ItemsImage;
          console.log(app.data.detail);
          app.data.describe = res.data[n].ItemsDescribe;
          that.setData({
            [`proList[${n}].id`]: app.data.id,
            [`proList[${n}].imagePath`]: app.data.imagePath,
            [`proList[${n}].title`]: app.data.title,
            [`proList[${n}].desc`]: app.data.describe
          })
        }
      },
      error: function (res) {
        console.log("failed");
        console.log(res.data.responseText);
      }
    })

  },


  toDetail: function(event) {
    app.data.id = event.currentTarget.dataset.id;
    console.log(event);
    wx.navigateTo({
      url: '../detail1/detail1'
    })

  },

})