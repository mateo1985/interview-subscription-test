import { Component, OnInit, ViewChild, ElementRef } from '@angular/core';
import { Validators, FormBuilder, FormGroup, FormArray, FormControl, NgForm } from '@angular/forms';
import { TopicsProviderService } from '../../services/topics-provider.service';
import { Topic } from '../../models/topic';
import { MatSelectionListChange, MatList, MatSelectionList } from '@angular/material/list';
import { CustomErrorStateMatcher } from './custom-error-state-matcher';
import { SubscriptionsService } from '../../services/subscriptions.service';
import { NotificationService } from '../../services/notification.service';

/**
 * This component is definetly too big
 * Should be splited
 * Additional directive can be created which would reset the values
 */

@Component({
  selector: 'app-subscriptions',
  templateUrl: './subscriptions.component.html',
  styleUrls: ['./subscriptions.component.scss']
})
export class SubscriptionsComponent implements OnInit {
  @ViewChild('ngForm') submissionNgForm: NgForm;
  @ViewChild(MatSelectionList) selectionListRef: MatSelectionList;
  public topics: Topic[];
  public matcher: CustomErrorStateMatcher;
  public submissionFormGroup: FormGroup;
  public isBusy: boolean = false;
  
  constructor(private formBuilder: FormBuilder,
              public topicsProviderService: TopicsProviderService,
              public subscriptionsService:SubscriptionsService,
              private notificationService: NotificationService) { 
    this.matcher = new CustomErrorStateMatcher();
    this.createSubmissionForm();
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
    if (!this.submissionFormGroup.valid) {
      return;
    }

    this.isBusy = true;
    let selectedTopics = this.selectedTopics();
    this.subscriptionsService.subscribe(selectedTopics, this.submissionFormGroup.get('email').value)
    .subscribe(response => {
      //console is only for demo purposes - it should not be here
      this.isBusy = false;
      console.log(response);
      this.notificationService.showSuccess("Subscription success", response.unsubscribeLink);
      this.resetValues();

    }, error => {
      this.isBusy = false;
      console.error(error);
      this.notificationService.showError("Something went wrong", error.error.message);
    });
  }

  ngOnInit() {
    this.topicsProviderService.Topics.then(topics => {
      this.topics = topics;
    }).catch(err => {
      console.log('Could not get topics', err);
      this.notificationService.showError("Something went wrong", "Could not get topics");
    });
  }

  private addTopic(topic: Topic): void {
    this.getTopicsControl().push(this.createItem(topic));
  }

  private removeTopic(topic: Topic): void {
    this.getTopicsControl().removeAt(this.getTopicsControl().value.findIndex(x => x.id === topic.id));
  }

  private getTopicsControl(): FormArray {
    return this.submissionFormGroup.get('topics') as FormArray;
  }

  private selectedTopics(): number[] {
    let topics = <FormArray>this.getTopicsControl();
    return topics.controls.map(x => x.value.id);
  }

  private resetValues() {
    this.submissionFormGroup.patchValue({
      email: ''
    });

    while(this.getTopicsControl().length !== 0) {
      this.getTopicsControl().removeAt(0);
    }

    this.selectionListRef.selectedOptions.clear()
  }

  private createSubmissionForm(): void {
    this.submissionFormGroup = this.formBuilder.group({
      email: ['', [Validators.required, Validators.email]],
      topics: this.formBuilder.array([], Validators.required)
    });
  }
}
