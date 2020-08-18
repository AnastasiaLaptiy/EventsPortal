import { ToastrService } from 'ngx-toastr';

export class ImgUtil{
    constructor(public toastr: ToastrService){}

    createImgPath = (serverPath: string) => {
        return `http://localhost:50618/${serverPath}`;
    }

    downloadImg(files){
        if (files.length === 0) {
            this.toastr.error('Image spot is empty!', 'Error');
            return;
          }
          let fileToUpload = <File>files[0];
          const formData = new FormData();
          formData.append('file', fileToUpload, fileToUpload.name);
          return formData;
    }
}