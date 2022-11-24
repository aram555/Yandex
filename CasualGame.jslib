mergeInto(LibraryManager.library, {

  Hello: function () {
    window.alert("Не Хватает Денег!");
  },

  GetData: function () {
    MyGameInstance.SendMessage('EventSystem', 'GetName', player.getName());
    MyGameInstance.SendMessage('EventSystem', 'GetPhoto', player.getPhoto("medium"));
  },

  RewardGame: function () {
    ysdk.feedback.canReview()
        .then(({ value, reason }) => {
            if (value) {
                ysdk.feedback.requestReview()
                    .then(({ feedbackSent }) => {
                        console.log(feedbackSent);
                    })
            } else {
                console.log(reason)
            }
        })
  },

  SaveExtern: function(date) {
    var dateString = UTF8ToString(date);
    var myobj = JSON.parse(dateString);
    player.setData(myobj);
  },

  LoadExtern: function(){
    player.getData().then(_date => {
      const myJSON = JSON.stringify(_date);
      MyGameInstance.SendMessage('EventSystem', 'SetPlayerInfo', myJSON);
    });
  },


});