const app = getApp();
// pages/detail1/detail1.js
Page({

  /**
   * 页面的初始数据
   */
  data: {
      image: '',
      detail: '',
  },

  /**
   * 生命周期函数--监听页面加载
   */
  onLoad: function(options) {
    var that = this;
    let n = app.data.id;
    console.log(n);
    wx.request({
      url: 'http://localhost:54039/actionapi/Items/get',
      header: { 'Content-Type': 'application/json' },
      method: 'GET',
      success: function (res) {
        console.log("成功");
        app.data.imagePath = res.data[app.data.id].ItemsImage;
        app.data.detail = res.data[app.data.id].ItemsDetail;
        that.setData({
          image: app.data.imagePath,
          detail: app.data.detail
        })
      },
      error: function (res) {
        console.log("failed");
        console.log(res.data.responseText);
      }
    })
      
  },

  /**
   * 生命周期函数--监听页面初次渲染完成
   */
  onReady: function() {

  },

  /**
   * 生命周期函数--监听页面显示
   */
  onShow: function() {
    
  },

  /**
   * 生命周期函数--监听页面隐藏
   */
  onHide: function() {

  },

  /**
   * 生命周期函数--监听页面卸载
   */
  onUnload: function() {

  },

  /**
   * 页面相关事件处理函数--监听用户下拉动作
   */
  onPullDownRefresh: function() {

  },

  /**
   * 页面上拉触底事件的处理函数
   */
  onReachBottom: function() {

  },

  /**
   * 用户点击右上角分享
   */
  onShareAppMessage: function() {

  }
})