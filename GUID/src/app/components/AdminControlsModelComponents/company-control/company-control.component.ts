import { Component } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { HttpserviceService } from '../../../../Services/httpservice.service';
import { Router } from '@angular/router';
import { LoginObject } from '../../../models/LoginObject';
import { sessionController } from '../../../logic/sessionLogic';
import { Session } from '../../../models/Session';
import { Company } from '../../../models/Company';
import { User } from '../../../models/User';

@Component({
  selector: 'app-company-control',
  templateUrl: './company-control.component.html',
  styleUrl: './company-control.component.css'
})
export class CompanyControlComponent<T> {
  
  //Gemmer brugerens input i variabler
  createForm = new FormGroup({
    CreateCompanyName: new FormControl<string>('', Validators.required),
    CreateCompanyEmail: new FormControl<string>('', Validators.required),
    CreateCompanyCVR: new FormControl<string>('', Validators.required),
  });

  updateForm = new FormGroup({
    SelectCompanyUpdate: new FormControl<string>('', Validators.required),
    UpdateCompanyName: new FormControl<string>('', Validators.required),
    UpdateCompanyEmail: new FormControl<string>('', Validators.required),
    UpdateCompanyCVR: new FormControl<string>('', Validators.required),
  })

  deleteForm = new FormGroup({
    SelectCompanyDelete: new FormControl<string>(''),
  })

  selectForm = new FormGroup({
    CompanySelect: new FormControl<string>('', Validators.required),
    SelectCompanyName: new FormControl<string>('', Validators.required),
    SelectCompanyEmail: new FormControl<string>('', Validators.required),
    SelectCompanyCVR: new FormControl<string>('', Validators.required),
  })
  
  public companyList:Company[] = new Array<Company>();

  constructor(private service:HttpserviceService<T>, private router:Router) {
  };

  

  GetListOfCompanies(){
    this.service.GetRequest<Company[]>("Company/GetAllCompanies").subscribe(company=> {
      this.companyList = company;
      console.log(company);
    })
  }

  create() {
    let company:Company = this.InputDataCreateCompany();
    this.service.PostRequest<Company>("Company/CreateCompany",company).subscribe(data => {
      if(data != null){
        this.createForm.reset()
        this.GetListOfCompanies()
      }
    })
  }

  update(){
    let company:Company = this.InputDataUpdateCompany();
    this.service.PutRequest<Company>("Company/UpdateCompany",company).subscribe(data => {
      if(data != null){
        this.updateForm.reset()
      }
    })
  }

  delete() {
    let company:Company = this.InputDataDeleteCompany()
    this.service.PostRequest<Company>("Company/DeleteCompany",company).subscribe(data => {
      if(data != null){
        this.deleteForm.reset()
        this.GetListOfCompanies()
      }
    })
  }

  InputDataCreateCompany(): Company {
    let company: Company = new Company();
    company.name = this.createForm.get('CreateCompanyName')?.value as string;
    company.email = this.createForm.get('CreateCompanyEmail')?.value as string;
    company.cvr = this.createForm.get('CreateCompanyCVR')?.value as string;
    company.users = new Array<User>();
    return company;
  }
  
  InputDataUpdateCompany():Company{
    var company:Company = this.companyList.find(ele => ele.name == this.updateForm.get("SelectCompanyUpdate")?.value) == undefined ? new Company() : this.companyList.find(ele => ele.name == this.updateForm.get("SelectCompanyUpdate")?.value) as Company;
    company.name = this.updateForm.get('UpdateCompanyName')?.value as string;
    company.email = this.updateForm.get('UpdateCompanyEmail')?.value as string;
    company.cvr = this.updateForm.get('UpdateCompanyCVR')?.value as string;
    console.log(company);
    return company;
  }

  InputDataDeleteCompany():Company{
    var company:Company = this.companyList.find(ele => ele.name == this.deleteForm.get("SelectCompanyDelete")?.value) == undefined ? new Company() : this.companyList.find(ele => ele.name == this.deleteForm.get("SelectCompanyDelete")?.value) as Company;
    return company;
  }

  selectOnChange(value:string | null):void{

    var company:Company =this.companyList.find(ele => ele.name == value) == undefined ? new Company() : this.companyList.find(ele => ele.name == value) as Company;
   
    if(company != new Company()){
       this.selectForm.setValue({CompanySelect:company.name,SelectCompanyName:company.name,SelectCompanyEmail:company.email,SelectCompanyCVR:company.cvr},{emitEvent:false})
     
    }
  }

  ngOnInit(){
    this.GetListOfCompanies();
    this.selectForm.controls["CompanySelect"].valueChanges.subscribe(value =>{
      if(value != ""){
        this.selectOnChange(value)
      }
    });
  }
}
