import {Component, EventEmitter, Input, OnInit, Output} from '@angular/core';
import {Observable} from "rxjs";
import {AdminService} from "../admin.service";
import {HttpEventType, HttpResponse} from "@angular/common/http";
import {environment} from "../../../environments/environment";

@Component({
  selector: 'app-image-upload',
  templateUrl: './image-upload.component.html',
  styleUrls: ['./image-upload.component.scss']
})
export class ImageUploadComponent implements OnInit {
  @Input() preview = '';    //for getting preview from parent component (edit-product component)
  @Output() uploadEvent = new EventEmitter<string>();     //for send message to parent compenent

  selectedFiles?: FileList;  //this will contain selected files for upload
  currentFile?: File;
  progress = 0;
  message = '';
  // preview = '';
  uploaded = false;
  imageInfos?: Observable<any>;
  baseServerUrl = environment.serverUrl;      //server url

  constructor(private uploadService: AdminService) {
  }

  ngOnInit(): void {
  }

  selectFile(event: any): void {
    this.message = '';
    this.preview = '';
    this.progress = 0;
    this.selectedFiles = event.target.files;      //get selected file in open dialog
    if (this.selectedFiles) {                   //if user has selected any file
      const file: File | null = this.selectedFiles.item(0);     //get first item of selected files
      if (file) {         //if first item is valid
        this.preview = '';
        this.currentFile = file;    //put it in currentFile variable
        const reader = new FileReader(); //create a file reader to read the local file to preview
        reader.onload = (e: any) => {
          this.preview = e.target.result;     //put file content in preview to show a preivew in image upload compoentn
        };
        reader.readAsDataURL(this.currentFile);  //reader reads all file data to preview
        this.upload();      //starting upload
      }
    }
  }

  upload(): void {
    this.progress = 0;
    if (this.selectedFiles) {
      const file: File | null = this.selectedFiles.item(0);
      if (file) {
        this.currentFile = file;
        this.uploadService.upload(this.currentFile).subscribe({     //call upload method from uploadservice to uploading currentFile
          next: (event: any) => {
            //the result would be either progress or upload response and we checked if result is progress then we show the value in progress bar
            if (event.type === HttpEventType.UploadProgress) {
              this.progress = Math.round((100 * event.loaded) / event.total);
            } else if (event instanceof HttpResponse) {     //if result would be response that means opreation is finished and image is uploaded or failed
              this.message = event.body.message;
              if (event.body.imagePath) {     //if uploading was successfull we should have image path in response body then we use it
                this.uploaded = true;
                this.uploadEvent.emit(event.body.imagePath)
              }

            }
          }, error: (err: any) => {
            console.log(err);
            this.progress = 0;
            if (err.error && err.error.message) {
              this.message = err.error.message;
            } else {
              this.message = 'Could not upload the image';
            }
            this.currentFile = undefined;

          }
        });
      }
      this.selectedFiles = undefined;
    }
  }
}
