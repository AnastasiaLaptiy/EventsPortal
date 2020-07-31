import { Component, OnInit, EventEmitter, Output } from '@angular/core';
import { HttpEventType } from '@angular/common/http';

import { UploadService } from '../shared/services/upload-service';

@Component({
  selector: 'app-upload-img',
  templateUrl: './upload-img.component.html',
  styles: [
  ]
})
export class UploadImgComponent implements OnInit {
  public progress: number;
  public message: string;
  @Output() public onUploadFinished = new EventEmitter();

  constructor(public uploadService: UploadService) { }

  ngOnInit(): void {
  }

  public uploadFile = (files) => {
    if (files.length === 0) {
      return;
    }

    let fileToUpload = <File>files[0];
    const formData = new FormData();
    formData.append('file', fileToUpload, fileToUpload.name);

    this.uploadService.UploadImage(formData)
      .subscribe(event => {
        if (event.type === HttpEventType.UploadProgress)
          this.progress = Math.round(100 * event.loaded / event.total);
        else if (event.type === HttpEventType.Response) {
          this.message = 'Upload success.';
          this.onUploadFinished.emit(event.body);
        }
      });
  }
}
