import { Component, OnInit, ViewChild } from '@angular/core';
import { Validators, FormBuilder, FormGroup, FormArray, FormControl, NgForm } from '@angular/forms';
import { TopicsProviderService } from '../../services/topics-provider.service';
import { Topic } from '../../models/topic';
import { MatSelectionListChange } from '@angular/material/list';
import { CustomErrorStateMatcher } from './custom-error-state-matcher';
import { SubscriptionsService } from '../../services/subscriptions.service';
import { NotificationService } from '../../services/notification.service';

@Component({
  selector: 'app-subscriptions',
  templateUrl: './subscriptions.component.html',
  styleUrls: ['./subscriptions.component.scss']
})
export class SubscriptionsComponent implements OnInit {
  @ViewChild('ngForm') submissionNgForm: NgForm;
  public topics: Topic[];
  public matcher: CustomErrorStateMatcher;
  public submissionFormGroup: FormGroup;
  
  constructor(private formBuilder: FormBuilder,
    public topicsProviderService: TopicsProviderService,
    public subscriptionsService:SubscriptionsService,
    private notificationService: NotificationService) { 
    this.matcher = new CustomErrorStateMatcher();
    this.createSubmissionForm();
  }

  private createSubmissionForm(): void {
    this.submissionFormGroup = this.formBuilder.group({
      email: ['', [Validators.required, Validators.email]],
      topics: this.formBuilder.array([], Validators.required)
    });
  }

  createItem(topic: Topic): FormGroup {
    return this.formBuilder.group(topic);
  }

  selectedTopicChanged(event: MatSelectionListChange): void {
    this.getTopicsControl().markAsDirty();
    if (event.option.selected) {
      this.addTopic(event.option.value);
      return;
    }
      
    this.removeTopic(event.option.value);
  }

  isTopicsInvalidState() {
    return this.matcher.isErrorState(this.getTopicsControl(), this.submissionNgForm);
  }

  submitForm(): void {
    this.notificationService.showSuccess("test 1", "test 2");
    return;
    let form =  this.submissionFormGroup;
    if (form.valid) {
      this.subscriptionsService.subscribe([1, 3, 5], this.submissionFormGroup.get('email').value)
      .subscribe(response => {
        console.log(response);
      }, error => {
        console.error(error);
      })
    }
  }

  ngOnInit() {
    this.topicsProviderService.Topics.then(topics => {
      this.topics = topics;
    }).catch(err => {
      console.log('Could not get topics', err);
    });
  }

  private addTopic(topic: Topic): void {
    let items = this.getTopicsControl();
    items.push(this.createItem(topic));
  }

  private removeTopic(topic: Topic): void {
    let items = this.getTopicsControl();
    items.removeAt(items.value.findIndex(x => x.id === topic.id));
  }

  private getTopicsControl(): FormArray {
    return this.submissionFormGroup.get('topics') as FormArray;
  }
}
