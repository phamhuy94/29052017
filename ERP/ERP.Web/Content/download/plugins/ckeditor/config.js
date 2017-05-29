/*
Copyright (c) 2003-2013, CKSource - Frederico Knabben. All rights reserved.
For licensing, see LICENSE.html or http://ckeditor.com/license
*/

CKEDITOR.editorConfig = function( config )
{
	// Define changes to default configuration here. For example:
	// config.language = 'fr';
    // config.uiColor = '#AADC6E';
    config.language = 'en';
    config.filebrowserBrowseUrl = "/Content/download/plugins/ckfinder/ckfinder.html";
    config.filebrowserImageUrl = "/Content/download/plugins/ckfinder/ckfinder.html?type=Images";
    config.filebrowserFlashUrl = "/Content/download/plugins/ckfinder/ckfinder.html?type=Flash";
    config.filebrowserUploadUrl = "/Content/download/plugins/ckfinder/core/connector/aspx/connector.aspx?command=QuickUpload&type=Files";
    config.filebrowserImageUploadUrl = "/Content/download/plugins/ckfinder/core/connector/aspx/connector.aspx?command=QuickUpload&type=Images";
    config.filebrowserFlashUploadUrl = "/Content/download/plugins/ckfinder/core/connector/aspx/connector.aspx?command=QuickUpload&type=Flash";

};
