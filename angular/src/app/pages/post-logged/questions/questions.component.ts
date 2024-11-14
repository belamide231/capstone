import { Component } from '@angular/core';
import { FeedsComponentComponent } from "../../../../components/feeds-component/feeds-component.component";
import { RequestPostComponent } from "../../../../components/request-post/request-post.component";
import { ActivitiesFeedsComponent } from "../../../../components/activities-feeds/activities-feeds.component";

@Component({
  selector: 'app-questions',
  standalone: true,
  imports: [FeedsComponentComponent, RequestPostComponent, ActivitiesFeedsComponent],
  templateUrl: './questions.component.html',
  styleUrl: './questions.component.sass'
})
export class QuestionsComponent {

}
