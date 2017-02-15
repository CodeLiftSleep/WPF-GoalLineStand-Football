"use strict";

angular.module('app.inbox').factory('InboxMessage', function($resource, APP_CONFIG){
   var InboxMessage = $resource(APP_CONFIG.apiRootUrl + '/messages.json/:id', {'id': '@_id'}, {
        get:{
            url: APP_CONFIG.apiRootUrl + '/message.json',
            isArray: false
        }
    });

    _.extend(InboxMessage.prototype, {
        selected: false,
        hasAttachments: function(){
            return (_.isArray(this.attachments) && this.attachments.length)
        },
        fullAttachmentsTootlip: function(){
            return 'FILES: ' + _.pluck(this.attachments, 'name').join(', ');
        },
        getBodyTeaser: function(){
            var clearBody  = this.body.replace(/<[^<>]+?>/gm, ' ').replace(/(\s{2}|\n)/gm, ' ');

            var teaserMaxLength = 55 - this.subject.length;

            return clearBody.length > teaserMaxLength ? clearBody.substring(0, teaserMaxLength) + '...' : clearBody;
        }
    });

    return InboxMessage;

});